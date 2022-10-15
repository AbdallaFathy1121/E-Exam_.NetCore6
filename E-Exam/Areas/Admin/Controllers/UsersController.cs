using E_Exam.Core.Models;
using E_Exam.Core.Services;
using E_Exam.Core.ViewModels;
using E_Exam.EF;
using E_Exam.Utility.Consts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace E_Exam.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = Roles.Admin)]
    public class UsersController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;
        private IUserService _userService;
        public UsersController(UserManager<ApplicationUser> userManager, 
            ApplicationDbContext context, IUserService userService)
        {
            _userManager = userManager;
            _context = context;
            _userService = userService;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _userService.GetAllUsersAsync();
            return View(result);
        }

        public async Task<IActionResult> BlockedUsers()
        {
            var result = await _userService.GetBlockedUsersAsync();
            return View("Index", result);
        }

        public async Task<IActionResult> Doctors()
        {
            var result = await _userService.GetDoctorsAsync();
            return View("Index", result);
        }

        public async Task<IActionResult> Students()
        {
            var result = await _userService.GetStudentsAsync();
            return View("Index", result);
        }

        public async Task<IActionResult> Delete(string id)
        {
            var user = await _userManager.FindByEmailAsync(id);
            if(user is not null)
            {
                await _userManager.DeleteAsync(user);
                TempData["Success"] = "Delete User Successfully!";

                return Json(new { success = true });
            }

            return Json(new { success = false, message = $"Not Found User With Email: {id}" });
        }

        [HttpPost]
        public async Task<IActionResult> ToggleBlockUser(string id)
        {
            var result = await _userService.ToggleBlockUserAsync(id);
            if (result.Success)
            {
                var user = await _userManager.FindByEmailAsync(id);
                TempData["Success"] = result.Message;
                
                _context.Update(user);
                await _context.SaveChangesAsync();

                return Json(new { success = true });
            }
            else
            {
                TempData["Error"] = result.Message;
                return Json(new { success = false });
            }
        }
    
    }
}
