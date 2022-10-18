using E_Exam.Core.Interfaces;
using E_Exam.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Exam.EF.Repositories
{
    public class ChapterRepository : BaseRepository<TbChapter>, IChapterRepository
    {
        private readonly ApplicationDbContext _context;
        public ChapterRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
