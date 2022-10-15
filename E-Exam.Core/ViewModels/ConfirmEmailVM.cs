using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Exam.Core.ViewModels
{
    public class ConfirmEmailVM
    {
        [Required]
        public string Token { get; set; }
        
        [Required]
        public string UserId { get; set; }
    }
}
