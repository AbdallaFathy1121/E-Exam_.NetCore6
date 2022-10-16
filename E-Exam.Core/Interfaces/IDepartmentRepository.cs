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
        Task<IEnumerable<TbDepartment>> GetDepartmentsThatNotInDepartmentLevel(int levelId);
        Task<IEnumerable<TbDepartment>> GetDepartmentsThatNotInDepartmentSubject(int subjectId, int levelId);
        Task<IEnumerable<TbDepartment>> GetDepartmentsWithByLevelId(int levelId);
    }
}
