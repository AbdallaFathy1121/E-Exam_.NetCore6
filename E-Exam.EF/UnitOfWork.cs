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
        public ILevelRepository TbLevels { get; private set; }
        public IDepartmentRepository TbDepartments { get; private set; }
        public IDepartmentLevelRepository TbDepartmentLevels { get; private set; }
        public ISubjectRepository TbSubjects { get; private set; }
        public ISubjectDepartmentRepository TbSubjectDepartments { get; private set; }


        public UnitOfWork(ApplicationDbContext context)
        {
            this.context = context;
            TbLevels = new LevelRepository(context);
            TbDepartments = new DepartmentRepository(context);
            TbDepartmentLevels = new DepartmentLevelRepository(context);
            TbSubjects = new SubjectRepository(context);
            TbSubjectDepartments = new SubjectDepartmentRepository(context);
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
