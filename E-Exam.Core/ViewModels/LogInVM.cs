using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Exam.Core.ViewModels
{
    public class LogInVM
    {
        [EmailAddress(ErrorMessage ="Invalid Email Address")]
        [Required(ErrorMessage ="Please Enter Your Email")]
        public string Email { get; set; }

        [Required(ErrorMessage ="Enter your Password")]
        public string Password { get; set; }
    }
}
