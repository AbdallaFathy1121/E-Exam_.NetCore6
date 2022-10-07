using E_Exam.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Exam.Core.ViewModels
{
    public class DepartmentVM
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please Enter Department Name")]
        public string Name { get; set; }

        public IEnumerable<TbDepartmentLevel>? DepartmentLevels { get; set; }
    }
}
