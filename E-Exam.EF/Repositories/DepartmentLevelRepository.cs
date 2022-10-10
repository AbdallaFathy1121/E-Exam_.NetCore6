using E_Exam.Core.Interfaces;
using E_Exam.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Exam.EF.Repositories
{
    public class DepartmentLevelRepository : BaseRepository<TbDepartmentLevel>, IDepartmentLevelRepository
    {
        private readonly ApplicationDbContext _context;
        public DepartmentLevelRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
