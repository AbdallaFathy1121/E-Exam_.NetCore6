using E_Exam.Core;
using E_Exam.Core.Interfaces;
using E_Exam.Core.Models;
using E_Exam.EF.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Exam.EF
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext context;
        public IBaseRepository<TbLevel> TbLevels { get; private set; }
        public IBaseRepository<TbDepartment> TbDepartments { get; private set; }
        public IBaseRepository<TbDepartmentLevel> TbDepartmentLevels { get; private set; }
        public UnitOfWork(ApplicationDbContext context)
        {
            this.context = context;
            TbLevels = new BaseRepository<TbLevel>(context);
            TbDepartments = new BaseRepository<TbDepartment>(context);
            TbDepartmentLevels = new BaseRepository<TbDepartmentLevel>(context);
        }

        public int Complete()
        {
            return context.SaveChanges();
        }
        public void Dispose()
        {
            context.Dispose();
        }
    }
}
