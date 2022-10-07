using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Exam.Core.Models
{
    public class TbDepartmentLevel
    {
        public int Id { get; set; }
        public int LevelId { get; set; }
        public int DepartmentId { get; set; }

        // Relations
        public virtual TbLevel? Level { get; set; }
        public virtual TbDepartment? Department { get; set; }

    }
}
