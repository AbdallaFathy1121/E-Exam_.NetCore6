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
        public IChapterRepository TbChapters { get; private set; }
        public IModelTypeRepository TbModelTypes { get; private set; }
        public IModelRepository TbModels { get; private set; }
        public IQuestionRepository TbQuestions { get; private set; }
        public IExamRepository TbExams { get; private set; }
        public IExamCollectionRepository TbExamCollections { get; private set; }


        public UnitOfWork(ApplicationDbContext context)
        {
            this.context = context;
            TbLevels = new LevelRepository(context);
            TbDepartments = new DepartmentRepository(context);
            TbDepartmentLevels = new DepartmentLevelRepository(context);
            TbSubjects = new SubjectRepository(context);
            TbSubjectDepartments = new SubjectDepartmentRepository(context);
            TbChapters = new ChapterRepository(context);
            TbModelTypes = new ModelTypeRepository(context);
            TbModels = new ModelRepository(context);
            TbQuestions = new QuestionRepository(context);
            TbExams = new ExamRepository(context);
            TbExamCollections = new ExamCollectionRepository(context);
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
