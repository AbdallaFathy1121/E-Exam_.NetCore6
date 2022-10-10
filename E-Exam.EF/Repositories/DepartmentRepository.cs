using E_Exam.Core.Interfaces;
using E_Exam.Core.Models;
using Microsoft.Data.SqlClient;
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


        public async Task<IEnumerable<TbDepartment>> GetDepartmentsThatNotInDepartmentLevel(int levelId)
        {
            var LevelIdParam = new SqlParameter("@LevelId", levelId);

            var result = await _context.TbDepartments
                .FromSqlRaw($"SPGetDepartmentsThatNotInDepartmentLevel @LevelId", LevelIdParam).ToListAsync();
            
            return result;
        }

        public async Task<IEnumerable<TbDepartment>> GetDepartmentsWithByLevelId(int levelId)
        {
            var LevelIdParam = new SqlParameter("@LevelId", levelId);

            var result = await _context.TbDepartments
                .FromSqlRaw($"SP_GetDepartmentsWithLevelId @LevelId", LevelIdParam).ToListAsync();

            return result;
        }
      
    }
}
