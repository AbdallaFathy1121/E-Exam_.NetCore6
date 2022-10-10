using E_Exam.Core;
using E_Exam.Core.Models;
using E_Exam.Core.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace E_Exam.Controllers
{
    public class UsersController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IUnitOfWork _unitOfWork;
        public UsersController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> RegisterStudent()
        {
            RegisterStudentVM model = new();
            model.Levels = await _unitOfWork.TbLevels.GetAllAsync();

            return View(model);
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
                EmailConfirmed = false,
            };

            try
            {
                var createUser = await _userManager.CreateAsync(user, model.Password);
                if (createUser.Succeeded)
                {
                    TempData["Success"] = "Create New User Successfully!";
                    return Redirect("/");
                }
                else
                {
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





    }
}
