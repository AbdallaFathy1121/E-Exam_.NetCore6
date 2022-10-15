using E_Exam.Core.Models;
using E_Exam.Core.ViewModels;
using E_Exam.Utility.Consts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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
        private readonly SignInManager<ApplicationUser> _signInManager;
        public UserService(UserManager<ApplicationUser> userManager, 
            RoleManager<IdentityRole> roleManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
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

        public async Task<List<UserVM>> GetAllUsersAsync()
        {
            var users = await _userManager.Users.Include(a => a.Level).Include(a => a.Department)
                .Where(a => a.Email != "admin@admin.com")
                .Select(u => new UserVM
                {
                    FullName = u.FullName,
                    Email = u.Email,
                    Photo = u.Photo,
                    Level = u.Level.Name,
                    Department = u.Department.Name,
                    EmailConfirmed = u.EmailConfirmed,
                    LockoutEnabled = u.LockoutEnabled
                }).ToListAsync();

            return users;
        }

        public async Task<List<UserVM>> GetBlockedUsersAsync()
        {
            var users = await _userManager.Users.Include(a => a.Level).Include(a => a.Department)
                .Where(a => a.Email != "admin@admin.com" && a.LockoutEnabled == false)
                .Select(u => new UserVM
                {
                    FullName = u.FullName,
                    Email = u.Email,
                    Photo = u.Photo,
                    Level = u.Level.Name,
                    Department = u.Department.Name,
                    EmailConfirmed = u.EmailConfirmed,
                    LockoutEnabled = u.LockoutEnabled
                }).ToListAsync();

            return users;
        }

        public async Task<List<UserVM>> GetDoctorsAsync()
        {
            var users = await _userManager.Users
                .Where(a => a.Email != "admin@admin.com" && a.Level == null)
                .Select(u => new UserVM
                {
                    FullName = u.FullName,
                    Email = u.Email,
                    Photo = u.Photo,
                    EmailConfirmed = u.EmailConfirmed,
                    LockoutEnabled = u.LockoutEnabled
                }).ToListAsync();

            return users;
        }

        public async Task<List<UserVM>> GetStudentsAsync()
        {
            var users = await _userManager.Users.Include(a => a.Level).Include(a => a.Department)
                .Where(a => a.Email != "admin@admin.com" && a.Level != null)
                .Select(u => new UserVM
                {
                    FullName = u.FullName,
                    Email = u.Email,
                    Level = u.Level.Name,
                    Department = u.Department.Name,
                    EmailConfirmed = u.EmailConfirmed,
                    LockoutEnabled = u.LockoutEnabled
                }).ToListAsync();

            return users;
        }

        public async Task<OperationResult> ToggleBlockUserAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user is not null)
            {
                user.LockoutEnabled = !user.LockoutEnabled;

                if (user.LockoutEnabled == false)
                {
                    await _userManager.UpdateSecurityStampAsync(user);
                    return OperationResult.Succeeded("Block User Successfully!");
                }

                if (user.LockoutEnabled == true)
                    return OperationResult.Succeeded("UnBlock User Successfully!");
            }
            
            return OperationResult.Error($"Not Found User With Email: {email}");
        }
    
        public async Task<OperationResult> RegisterAsync(ApplicationUser user, string password)
        {
            try
            {
                var createUser = await _userManager.CreateAsync(user, password);
                if (createUser.Succeeded)
                {
                    return OperationResult.Succeeded("Register Successfully!, Please Confirm your Email");
                }
                else
                {
                    var errors = createUser.Errors.FirstOrDefault().Description;
                    return OperationResult.Error(errors);
                }
            }
            catch (Exception ex)
            {
                return OperationResult.Error(ex.Message);
            }
        }

        public async Task<OperationResult> LoginAsync(LogInVM model)
        {
            var existedUser = await _userManager.FindByEmailAsync(model.Email);
            if (existedUser == null)
            {
                return OperationResult.Error("Invalid Email");
            }
            else if (existedUser.LockoutEnabled == false)
            {
                return OperationResult.Error("This Account Has been Blocked");
            }
            else
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, true, true);
                if (result.Succeeded)
                {
                    return OperationResult.Succeeded("Login Successfully!");
                }
                else if (result.IsNotAllowed)
                {
                    return OperationResult.Error("You must Confirm the email, To be able to login");
                }
                else
                {
                    return OperationResult.Error("Invalid Password");
                }
            }
        }

        public async Task<OperationResult> ConfirmEmailAsync(ConfirmEmailVM model)
        {
            var user = await _userManager.FindByIdAsync(model.UserId);
            if (user is not null)
            {
                if (!user.EmailConfirmed)
                {
                    var result = await _userManager.ConfirmEmailAsync(user, model.Token);
                    if (result.Succeeded)
                    {
                        return OperationResult.Succeeded("Your Email confirmed Successfully!");
                    }
                    else
                    {
                        var errors = result.Errors.FirstOrDefault().Description;
                        return OperationResult.Error(errors);
                    }
                }
                else
                {
                    return OperationResult.Error("Your Email already confirmed");
                }
            }
            else
            {
                return OperationResult.Error($"Notfound User With ID: {model.UserId}");
            }
        }
    }
}
