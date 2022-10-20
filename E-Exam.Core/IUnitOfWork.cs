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
        ILevelRepository TbLevels { get; }
        IDepartmentRepository TbDepartments { get; }
        IDepartmentLevelRepository TbDepartmentLevels { get; }
        ISubjectRepository TbSubjects { get; }
        ISubjectDepartmentRepository TbSubjectDepartments { get; }
        IChapterRepository TbChapters { get; }
        IModelTypeRepository TbModelTypes { get; }
        IModelRepository TbModels { get; }
        IQuestionRepository TbQuestions { get; }


        int Complete();
    }
}
