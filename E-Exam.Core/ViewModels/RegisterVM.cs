using E_Exam.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Exam.Core.ViewModels
{
    public class RegisterStudentVM
    {
        [Required(ErrorMessage = "Please Enter Your Name")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "You Should Type Email Address")]
        public string Email { get; set; }
        public int LevelId { get; set; }
        public int DepartmentId { get; set; }
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Confirm Password dosent match with Password")]
        public string ConfirmPassword { get; set; }


        // Relations
        public IEnumerable<TbDepartment>? Departments { get; set; } = new List<TbDepartment>();
    }
}
