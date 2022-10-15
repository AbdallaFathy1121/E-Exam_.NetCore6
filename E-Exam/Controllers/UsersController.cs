using E_Exam.Core;
using E_Exam.Core.Models;
using E_Exam.Core.Services;
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
        private IUserService _userService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEmailSender _emailSender;
        public UsersController(UserManager<ApplicationUser> userManager, IUnitOfWork unitOfWork, 
            SignInManager<ApplicationUser> signInManager, 
            IEmailSender emailSender, IUserService userService)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _userService = userService;
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

            var createUser = await _userService.RegisterAsync(user, model.Password);
            if (createUser.Success)
            {
                // Create Link
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var url = Url.Action("ConfirmEmail", "Users", new { token = token, userId = user.Id }, Request.Scheme);
                // Send Email
                string formatUrl = $"<h3> To confirm email, you should <a href='{url}'> Click here </a> </h3>";
                _emailSender.SendEmail(user.Email, "Email Confirmation", formatUrl);

                TempData["Success"] = createUser.Message;
                return RedirectToAction("LogIn");
            }
            else
            {
                TempData["Error"] = createUser.Message;
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

            var createUser = await _userService.RegisterAsync(user, model.Password);
            if (createUser.Success)
            {
                // add user to Role Doctor
                await _userManager.AddToRoleAsync(user, Roles.Doctor);
                // Create Link
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var url = Url.Action("ConfirmEmail", "Users", new { token = token, userId = user.Id }, Request.Scheme);
                // Send Email
                string formatUrl = $"<h3> To confirm email, you should <a href='{url}'> Click here </a> </h3>";
                _emailSender.SendEmail(user.Email, "Email Confirmation", formatUrl);

                TempData["Success"] = createUser.Message;
                return RedirectToAction("LogIn");
            }
            else
            {
                TempData["Error"] = createUser.Message;
                return View("RegisterDoctor", model);
            }
        }

        [HttpPost]
        public async Task<IActionResult> LogIn(LogInVM model, string? returnUrl)
        {
            if(!ModelState.IsValid)
                return View(model);

            var result = await _userService.LoginAsync(model);
            if (result.Success)
            {
                TempData["Success"] = result.Message;

                if (!string.IsNullOrEmpty(returnUrl))
                    return LocalRedirect(returnUrl);
                else
                    return Redirect("/");
            }
            else
            {
                TempData["Error"] = result.Message;
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
                var result = await _userService.ConfirmEmailAsync(model);
                if (result.Success)
                    TempData["Success"] = result.Message;
                else
                    TempData["Error"] = result.Message;
            }

            return RedirectToAction("LogIn");
        }

    }
}
