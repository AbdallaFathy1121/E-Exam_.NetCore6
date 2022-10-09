using E_Exam.Core.Interfaces;
using E_Exam.Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Exam.EF.Repositories
{
    public class DepartmentRepository : BaseRepository<TbDepartment>, IDepartmentRepository
    {
        private readonly ApplicationDbContext _context;
        public DepartmentRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }


        public IEnumerable<TbDepartment> GetLeftJoinWithTbDepartmentLevel(int levelId)
        {
            var department = (from d in _context.TbDepartments
                              join dl in _context.TbDepartmentLevels
                              on d.Id equals dl.DepartmentId
                              into groub
                              from res in groub.DefaultIfEmpty()
                              where res.LevelId != levelId
                              select d).Distinct().AsNoTracking().ToList();

            return department;
        }

        public IEnumerable<TbDepartment> GetDepartmentsWithByLevelId(int levelId)
        {
            var department = (from d in _context.TbDepartments
                              join dl in _context.TbDepartmentLevels
                              on d.Id equals dl.DepartmentId
                              into groub
                              from res in groub.DefaultIfEmpty()
                              where res.LevelId == levelId
                              select d).Distinct().AsNoTracking().ToList();

            return department;
        }
      
    }
}
