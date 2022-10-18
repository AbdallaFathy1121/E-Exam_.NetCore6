using E_Exam.Core.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Exam.EF
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) :base(options)
        {
        }

        public DbSet<TbLevel> TbLevels { get; set; }
        public DbSet<TbDepartment> TbDepartments { get; set; }
        public DbSet<TbDepartmentLevel> TbDepartmentLevels { get; set; }
        public DbSet<TbSubject> TbSubjects { get; set; }
        public DbSet<TbSubjectDepartment> TbSubjectDepartments { get; set; }
        public DbSet<TbChapter> TbChapters { get; set; }

    }
}
