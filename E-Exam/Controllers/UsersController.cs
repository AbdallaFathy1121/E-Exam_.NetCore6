using E_Exam.Core;
using E_Exam.Core.Consts;
using E_Exam.Core.Models;
using E_Exam.Core.ViewModels;
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
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IUnitOfWork _unitOfWork;
        public UsersController(UserManager<ApplicationUser> userManager, 
            RoleManager<IdentityRole> roleManager, IUnitOfWork unitOfWork, 
            SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _unitOfWork = unitOfWork;
            _signInManager = signInManager;
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
                    TempData["Success"] = "Register Successfully!";
                    return Redirect("/");
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
                    await _userManager.AddToRoleAsync(user, Roles.Doctor);
                    TempData["Success"] = "Register Successfully!";
                    return Redirect("/");
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



    }
}
