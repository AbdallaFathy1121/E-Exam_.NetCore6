using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Exam.Core.Models
{
    public class TbSubjectDepartment
    {
        public int Id { get; set; }
        public int SubjectId { get; set; }
        public int DepartmentId { get; set; }

        // Relations
        public virtual TbSubject? Subject { get; set; }
        public virtual TbDepartment? Department { get; set; }
    }
}
