using E_Exam.Core.Models;
using E_Exam.Utility.Consts;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Exam.Core.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public UserService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task Initialize()
        {
            if(!await _roleManager.RoleExistsAsync(Roles.Admin))
            {
                await _roleManager.CreateAsync(new IdentityRole(Roles.Admin));
            }
            if (!await _roleManager.RoleExistsAsync(Roles.Doctor))
            {
                await _roleManager.CreateAsync(new IdentityRole(Roles.Doctor));
            }

            var adminEmail = "admin@admin.com";
            if(await _userManager.FindByEmailAsync(adminEmail) == null)
            {
                ApplicationUser user = new()
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    FullName = "Admin",
                    EmailConfirmed = true
                };

                await _userManager.CreateAsync(user, "Abdalla0159357");
                await _userManager.AddToRoleAsync(user, Roles.Admin);
            }
        }
        





    }
}
