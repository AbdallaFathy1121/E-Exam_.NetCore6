using E_Exam.Core;
using E_Exam.Core.Models;
using E_Exam.Core.ViewModels;
using E_Exam.Utility.Consts;
using E_Exam.Utility.EmailSender;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace E_Exam.Controllers
{
    public class UsersController : Controller
    {
        // file size and extension
        private new List<string> _allowedExtenstions = new() { ".jpg", ".png" };
        private long _maxAllowedPosterSize = 1048576 * 2; // 2MB

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEmailSender _emailSender;
        public UsersController(UserManager<ApplicationUser> userManager, IUnitOfWork unitOfWork, 
            SignInManager<ApplicationUser> signInManager, IEmailSender emailSender)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _signInManager = signInManager;
            _emailSender = emailSender;
        }

        public async Task<IActionResult> RegisterStudent()
        {
            RegisterStudentVM model = new();
            model.Levels = await _unitOfWork.TbLevels.GetAllAsync();

            return View(model);
        }

        public IActionResult RegisterDoctor()
        {
            return View();
        }

        public IActionResult LogIn()
        {
            return View();
        }

        public async Task<IActionResult> GetDepartmentsByLevelId(int levelId)
        {
            var departments = await _unitOfWork.TbDepartments.GetDepartmentsWithByLevelId(levelId);

            return Ok(departments);
        }

        [HttpPost]
        public async Task<IActionResult> SaveStudent(RegisterStudentVM model)
        {
            if (!ModelState.IsValid)
            {
                model.Levels = await _unitOfWork.TbLevels.GetAllAsync();
                return View("RegisterStudent", model);
            }

            var findByEmail = await _userManager.FindByEmailAsync(model.Email);
            if (findByEmail is not null)
            {
                TempData["Error"] = "This Email already Existing";
                model.Levels = await _unitOfWork.TbLevels.GetAllAsync();
                return View("RegisterStudent", model);
            }

            ApplicationUser user = new()
            {
                UserName = model.Email,
                Email = model.Email,
                FullName = model.FullName,
                LevelId = model.LevelId,
                DepartmentId = model.DepartmentId,
            };

            try
            {
                var createUser = await _userManager.CreateAsync(user, model.Password);
                if (createUser.Succeeded)
                {
                    // Create Link
                    var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var url = Url.Action("ConfirmEmail", "Users", new { token = token, userId = user.Id }, Request.Scheme);
                    // Send Email
                    string formatUrl = $"<h3> To confirm email, you should <a href='{url}'> Click here </a> </h3>";
                    _emailSender.SendEmail(user.Email, "Email Confirmation", formatUrl);

                    TempData["Success"] = "Register Successfully!, Please Confirm your Email";
                    return RedirectToAction("LogIn");
                }
                else
                {
                    var errors = createUser.Errors.ToArray();
                    TempData["Error"] = errors;
                    model.Levels = await _unitOfWork.TbLevels.GetAllAsync();
                    return View("RegisterStudent", model);
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                model.Levels = await _unitOfWork.TbLevels.GetAllAsync();
                return View("RegisterStudent", model);
            }
        }

        [HttpPost]
        public async Task<IActionResult> SaveDoctor(RegisterDoctorVM model)
        {
            if (!ModelState.IsValid)
                return View("RegisterDoctor", model);

            if (!_allowedExtenstions.Contains(Path.GetExtension(model.Photo.FileName).ToLower()))
            {
                TempData["Error"] = "Only .png and .jpg images are allowed";
                return View("RegisterDoctor", model);
            }

            if (model.Photo.Length > _maxAllowedPosterSize)
            {
                TempData["Error"] = "Max allowed size for poster is 1MB";
                return View("RegisterDoctor", model);
            }

            var findByEmail = await _userManager.FindByEmailAsync(model.Email);
            if (findByEmail is not null)
            {
                TempData["Error"] = "This Email already Existing";
                return View("RegisterDoctor", model);
            }

            // Convert Photo from IFormFile to byte[]
            using var dataStream = new MemoryStream();
            await model.Photo.CopyToAsync(dataStream);

            ApplicationUser user = new()
            {
                UserName = model.Email,
                Email = model.Email,
                FullName = model.FullName,
                Photo = dataStream.ToArray()
            };

            try
            {
                var createUser = await _userManager.CreateAsync(user, model.Password);
                if (createUser.Succeeded)
                {
                    // add user to Role Doctor
                    await _userManager.AddToRoleAsync(user, Roles.Doctor);
                    // Create Link
                    var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var url = Url.Action("ConfirmEmail", "Users", new { token = token, userId = user.Id }, Request.Scheme);
                    // Send Email
                    string formatUrl = $"<h3> To confirm email, you should <a href='{url}'> Click here </a> </h3>";
                    _emailSender.SendEmail(user.Email, "Email Confirmation", formatUrl);

                    TempData["Success"] = "Register Successfully!, Please Confirm your Email";
                    return RedirectToAction("LogIn");
                }
                else
                {
                    var errors = createUser.Errors.ToArray();
                    TempData["Error"] = errors;
                    return View("RegisterDoctor", model);
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return View("RegisterDoctor", model);
            }

        }

        [HttpPost]
        public async Task<IActionResult> LogIn(LogInVM model, string? returnUrl)
        {
            if(!ModelState.IsValid)
                return View(model);

            var existedUser = await _userManager.FindByEmailAsync(model.Email);
            if (existedUser == null)
            {
                TempData["Error"] = "Invalid Email or Password";
                return View(model);
            }

            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, true, true);
            if (result.Succeeded)
            {
                if (!string.IsNullOrEmpty(returnUrl))
                    return LocalRedirect(returnUrl);
                else
                    return Redirect("/");
            }
            else if (result.IsNotAllowed)
            {
                TempData["Error"] = "You must Confirm the email, To be able to login";
            }

            return View(model);
        }

        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();

            return Redirect("/");
        }

        public async Task<IActionResult> ConfirmEmail(ConfirmEmailVM model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(model.UserId);
                if (user is not null)
                {
                    if (!user.EmailConfirmed)
                    {
                        var result = await _userManager.ConfirmEmailAsync(user, model.Token);
                        if (result.Succeeded)
                        {
                            TempData["Success"] = "Your Email confirmed Successfully!";
                        }
                        else
                        {
                            var errors = result.Errors.ToArray();
                            TempData["Error"] = errors;
                        }
                    }
                    else
                    {
                        TempData["Warning"] = "Your Email already confirmed";
                    }
                }
                else
                {
                    TempData["Error"] = $"Notfound User With ID: {model.UserId}";
                }
            }

            return RedirectToAction("LogIn");
        }

    }
}
