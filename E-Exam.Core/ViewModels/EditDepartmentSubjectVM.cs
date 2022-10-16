using E_Exam.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Exam.Core.ViewModels
{
    public class EditDepartmentSubjectVM
    {
        public IEnumerable<TbSubjectDepartment>? LstSubjectDepartments { get; set; }
        public IEnumerable<DepartmentVM>? LstDepartments { get; set; }
    }
}
