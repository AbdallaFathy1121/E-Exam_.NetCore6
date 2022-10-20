using E_Exam.Core.Interfaces;
using E_Exam.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Exam.EF.Repositories
{
    public class QuestionRepository : BaseRepository<TbQuestion>, IQuestionRepository
    {
        private readonly ApplicationDbContext _context;
        public QuestionRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
