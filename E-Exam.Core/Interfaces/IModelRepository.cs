using E_Exam.Core.Models;
using E_Exam.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Exam.Core.Interfaces
{
    public interface IModelRepository: IBaseRepository<TbModel>
    {
        Task<IEnumerable<ManageModelsPageVM>> GetAllModelsBySubjectIdAsync(int subjectId);
    }
}
