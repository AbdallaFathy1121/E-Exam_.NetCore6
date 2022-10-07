using E_Exam.Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Exam.EF
{
    public class ApplicationDbContext : DbContext
    {
        internal readonly object model;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) :base(options)
        {
        }

        public virtual DbSet<TbLevel> TbLevels { get; set; }
        public virtual DbSet<TbDepartment> TbDepartments { get; set; }
        public virtual DbSet<TbDepartmentLevel> TbDepartmentLevels { get; set; }


    }
}
