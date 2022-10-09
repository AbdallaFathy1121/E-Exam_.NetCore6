using E_Exam.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Exam.Core.Interfaces
{
    public interface IDepartmentRepository : IBaseRepository<TbDepartment>
    {
        IEnumerable<TbDepartment> GetLeftJoinWithTbDepartmentLevel(int levelId);
        IEnumerable<TbDepartment> GetDepartmentsWithByLevelId(int levelId);
    }
}
