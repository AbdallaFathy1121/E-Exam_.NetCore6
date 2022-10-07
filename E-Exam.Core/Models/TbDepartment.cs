using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Exam.Core.Models
{
    public class TbDepartment
    {
        public TbDepartment()
        {
            DepartmentLevels = new HashSet<TbDepartmentLevel>();
        }

        public int Id { get; set; }

        [Required(ErrorMessage ="Please Enter Department Name")]
        public string Name { get; set; }

        // Relations
        public virtual ICollection<TbDepartmentLevel> DepartmentLevels { get; set; }
    }
}
