using E_Exam.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Exam.Core.ViewModels
{
    public class SubjectVM
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Enter Subject Name")]
        public string Name { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }
        public int LevelId { get; set; }

        // Relations
        public ApplicationUser? User { get; set; }
        public TbLevel? Level { get; set; }
        public ICollection<TbSubjectDepartment>? SubjectDepartments { get; set; }
    }
}
