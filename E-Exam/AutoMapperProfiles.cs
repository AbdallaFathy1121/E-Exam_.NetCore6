using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using E_Exam.Core.ViewModels;
using E_Exam.Core.Models;


namespace File_Sharing
{
    public class LevelProfile : Profile
    {
        public LevelProfile()
        {
            CreateMap<LevelVM, TbLevel>();
                //.ForMember(x => x.CreationDate, op => op.Ignore())
                //.ForMember(x => x.UploadId, op => op.Ignore());

            CreateMap<TbLevel, LevelVM>();
        }
    }

    public class DepartmentProfile : Profile
    {
        public DepartmentProfile()
        {
            CreateMap<DepartmentVM, TbDepartment>();
            //.ForMember(x => x.CreationDate, op => op.Ignore())
            //.ForMember(x => x.UploadId, op => op.Ignore());

            CreateMap<TbDepartment, DepartmentVM>();
        }
    }

    public class SubjectProfile : Profile
    {
        public SubjectProfile()
        {
            CreateMap<SubjectVM, TbSubject>();
            //.ForMember(x => x.CreationDate, op => op.Ignore())
            //.ForMember(x => x.UploadId, op => op.Ignore());

            CreateMap<TbSubject, SubjectVM>();
        }
    }

    public class ChapterProfile : Profile
    {
        public ChapterProfile()
        {
            CreateMap<ChapterVM, TbChapter>();
            //.ForMember(x => x.CreationDate, op => op.Ignore())
            //.ForMember(x => x.UploadId, op => op.Ignore());

            CreateMap<TbChapter, ChapterVM>();
        }
    }

    public class ModelTypeProfile : Profile
    {
        public ModelTypeProfile()
        {
            CreateMap<ModelTypeVM, TbModelType>();
            //.ForMember(x => x.CreationDate, op => op.Ignore())
            //.ForMember(x => x.UploadId, op => op.Ignore());

            CreateMap<TbModelType, ModelTypeVM>();
        }
    }

    public class QuestionProfile : Profile
    {
        public QuestionProfile()
        {
            CreateMap<QuestionVM, TbQuestion>();
            //.ForMember(x => x.CreationDate, op => op.Ignore())
            //.ForMember(x => x.UploadId, op => op.Ignore());

            CreateMap<TbQuestion, QuestionVM>();
        }
    }

    public class ExamProfile : Profile
    {
        public ExamProfile()
        {
            CreateMap<ExamVM, TbExam>();
            //.ForMember(x => x.CreationDate, op => op.Ignore())
            //.ForMember(x => x.UploadId, op => op.Ignore());

            CreateMap<TbExam, ExamVM>();
        }
    }

    public class ExamCollectionsProfile : Profile
    {
        public ExamCollectionsProfile()
        {
            CreateMap<ExamCollectionVM, TbExamCollection>();
            //.ForMember(x => x.CreationDate, op => op.Ignore())
            //.ForMember(x => x.UploadId, op => op.Ignore());

            CreateMap<TbExamCollection, ExamCollectionVM>();
        }
    }


}
