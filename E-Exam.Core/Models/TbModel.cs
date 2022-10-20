using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Exam.Core.Models
{
    public class TbModel
    {
        public int Id { get; set; }
        public int SubjectId { get; set; }
        public int ModelTypeId { get; set; }
        public int ChapterId { get; set; }
        public DateTime? CreationDate { get; set; } = DateTime.UtcNow;

        // Relations
        public TbSubject? Subject { get; set; }
        public TbModelType? ModelType { get; set; }
        public TbChapter? Chapter { get; set; }

    }
}
