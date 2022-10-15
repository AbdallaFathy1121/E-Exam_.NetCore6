using E_Exam.Core.Models;
using E_Exam.Core.ViewModels;
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
        public UsersController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }


        public async Task<IActionResult> Index()
        {
            var users = await _userManager.Users.Include(a=>a.Level).Include(a=>a.Department)
                .Where(a=>a.Email != "admin@admin.com")
                .Select(u => new UserVM
            {
                FullName = u.FullName,
                Email = u.Email,
                Photo = u.Photo,
                Level = u.Level.Name,
                Department = u.Department.Name,
                EmailConfirmed = u.EmailConfirmed
            }).ToListAsync();

            return View(users);
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


    
    
    
    
    
    }
}
