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
        [Required(ErrorMessage = "Please Enter your Name")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "You Should Type Email Address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please Select your Level")]
        public int LevelId { get; set; }

        [Required(ErrorMessage = "Please Select your Department")]
        public int DepartmentId { get; set; }

        [Required(ErrorMessage ="Please Enter your Password")]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Confirm Password dosent match with Password")]
        public string ConfirmPassword { get; set; }


        // Relations
        public IEnumerable<TbDepartment>? Departments { get; set; } = new List<TbDepartment>();
        public IEnumerable<TbLevel>? Levels { get; set; }
    }
}
