using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Exam.Core.ViewModels
{
    public class ExamVM
    {
        public int Id { get; set; }
        public string ExamName { get; set; }
        public int SubjectId { get; set; }
        public string? AccessCode { get; set; }
        public DateTime StartDateTime { get; set; }
        public int Duration { get; set; }
    }
}
