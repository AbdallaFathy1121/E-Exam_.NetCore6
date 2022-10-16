using E_Exam.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Exam.Core.ViewModels
{
    public class SubjectPageVM
    {
        public IEnumerable<SubjectVM>? LstSubjects { get; set; }
        public IEnumerable<TbSubjectDepartment>? LstSubjectDepartments { get; set; }
    }
}
