using E_Exam.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Exam.Core.ViewModels
{
    public class UserVM
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public byte[]? Photo { get; set; }
        public bool EmailConfirmed { get; set; } = false;
        public string? Level { get; set; }
        public string? Department { get; set; }
    }
}
