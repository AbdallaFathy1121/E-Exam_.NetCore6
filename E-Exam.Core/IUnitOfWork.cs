using E_Exam.Core.Interfaces;
using E_Exam.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Exam.Core
{
    public interface IUnitOfWork : IDisposable
    {
        IBaseRepository<TbLevel> TbLevels { get; }
        IDepartmentRepository TbDepartments { get; }
        IBaseRepository<TbDepartmentLevel> TbDepartmentLevels { get; }

        int Complete();
    }
}
