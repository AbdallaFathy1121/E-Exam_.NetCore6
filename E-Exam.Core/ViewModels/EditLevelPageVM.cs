using E_Exam.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Exam.Core.ViewModels
{
    public class EditDepartmentLevel
    {
        public IEnumerable<TbDepartmentLevel>? LstDepartmentLevel { get; set; }
        public IEnumerable<DepartmentVM>? LstDepartmentVM { get; set; }
    }
}
