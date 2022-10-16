using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Exam.Core.Models
{
    public class TbSubject
    {
        public TbSubject()
        {
            SubjectDepartments = new HashSet<TbSubjectDepartment>();
        }
        public int Id { get; set; }

        [Required(ErrorMessage ="Enter Subject Name")]
        public string Name { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }
        public int LevelId { get; set; }

        // Relations
        public virtual ApplicationUser? User { get; set; }
        public virtual TbLevel? Level { get; set; }
        public virtual ICollection<TbSubjectDepartment>? SubjectDepartments { get; set; }

    }
}
