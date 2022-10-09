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

            ViewBag.Levels = await _unitOfWork.TbLevels.GetAllAsync();

            return View(model);
        }

        public IActionResult GetDepartmentsByLevelId(int levelId)
        {
            var departments = _unitOfWork.TbDepartments.GetDepartmentsWithByLevelId(levelId);

            return Ok(departments);
        }







    }
}
