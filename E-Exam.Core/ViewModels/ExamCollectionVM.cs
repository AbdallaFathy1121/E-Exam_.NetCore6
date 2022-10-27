using E_Exam.Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Exam.Core.ViewModels
{
    public class ExamCollectionVM
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="Enter Number Of Question")]
        public int NumberOfQuestions { get; set; }
        public int ExamId { get; set; }
        public int ChapterId { get; set; }
        public int ModelTypeId { get; set; }
        public int SubjectId { get; set; }

        // Relations
        public TbExam? Exam { get; set; }
        public TbChapter? Chapter { get; set; }
        public TbModelType? ModelType { get; set; }
        public TbSubject? Subject { get; set; }
    }
}
