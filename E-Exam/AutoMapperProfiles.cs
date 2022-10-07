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

}
