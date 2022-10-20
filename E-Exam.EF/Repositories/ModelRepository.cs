using E_Exam.Core.Interfaces;
using E_Exam.Core.Models;
using E_Exam.Core.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace E_Exam.EF.Repositories
{
    public class ModelRepository : BaseRepository<TbModel>, IModelRepository
    {
        private readonly ApplicationDbContext _context;
        public ModelRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }


        public async Task<IEnumerable<ManageModelsPageVM>> GetAllModelsBySubjectIdAsync(int subjectId)
        {
            var result = await _context.TbModels
                .Include(a => a.Subject)
                .Include(a => a.Chapter)
                .Include(a => a.ModelType)
                .Where(a => a.SubjectId == subjectId)
                .Select(a => new ManageModelsPageVM
                {
                    Id = a.Id,
                    SubjectId = subjectId,
                    ChapterId = a.ChapterId,
                    ModelTypeId = a.ModelTypeId,
                    SubjectName = a.Subject.Name,
                    ChapterName = a.Chapter.Name,
                    ModelType = a.ModelType.Type,
                }).ToListAsync();

            return result;
        }
    }
}
