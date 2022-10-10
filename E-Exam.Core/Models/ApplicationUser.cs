using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Exam.Core.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }
        public byte[]? Photo { get; set; }
        public int? LevelId { get; set; }
        public int? DepartmentId { get; set; }

        // Relations
        public TbLevel? Level { get; set; }
        public TbDepartment? Department { get; set; }
    }
}
