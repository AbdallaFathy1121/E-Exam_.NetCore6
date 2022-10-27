using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Exam.Core.Models
{
    public class TbExam
    {
        public TbExam()
        {
            ExamCollections = new HashSet<TbExamCollection>();
        }

        public int Id { get; set; }
        public string ExamName { get; set; }
        public int SubjectId { get; set; }
        public string? AccessCode { get; set; } = Guid.NewGuid().ToString();
        public DateTime StartDateTime { get; set; }
        public int Duration { get; set; }

        // Relations
        public DbSet<TbSubject>? Subject { get; set; }
        public ICollection<TbExamCollection>? ExamCollections { get; set; }
    }
}
