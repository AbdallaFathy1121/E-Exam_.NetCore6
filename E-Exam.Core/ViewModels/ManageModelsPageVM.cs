using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Exam.Core.ViewModels
{
    public class ManageModelsPageVM
    {
        public int Id { get; set; }
        public int SubjectId { get; set; }
        public string? SubjectName { get; set; }
        public int ModelTypeId { get; set; }
        public string? ModelType { get; set; }
        public int ChapterId { get; set; }
        public string? ChapterName { get; set; }
    }
}
