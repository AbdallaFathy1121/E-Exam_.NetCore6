using E_Exam.Core.Models;
using E_Exam.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Exam.Core.Services
{
    public interface IUserService
    {
        Task Initialize();
        Task<List<UserVM>> GetAllUsersAsync();
        Task<List<UserVM>> GetBlockedUsersAsync();
        Task<List<UserVM>> GetDoctorsAsync();
        Task<List<UserVM>> GetStudentsAsync();
        Task<OperationResult> ToggleBlockUserAsync(string email);
        Task<OperationResult> RegisterAsync(ApplicationUser user, string password);
        Task<OperationResult> LoginAsync(LogInVM model);
        Task<OperationResult> ConfirmEmailAsync(ConfirmEmailVM model);
    }
}
