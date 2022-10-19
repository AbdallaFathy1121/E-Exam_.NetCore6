using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Exam.Core.ViewModels
{
    public class ModelTypeVM
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="Enter Type")]
        [MaxLength(1, ErrorMessage = "You must enter only one letter")]
        public string Type { get; set; }
    }
}
