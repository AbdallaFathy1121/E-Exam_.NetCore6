using E_Exam.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Exam.Core.ViewModels
{
    public class LevelVM
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please Enter Level Name")]
        public string Name { get; set; }

        public ICollection<TbDepartmentLevel>? DepartmentLevels { get; set; }

    }
}
