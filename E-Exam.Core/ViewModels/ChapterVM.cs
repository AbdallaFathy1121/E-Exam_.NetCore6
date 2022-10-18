using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Exam.Core.ViewModels
{
    public class ChapterVM
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="Enter Chapter Name")]
        public string Name { get; set; }
    }
}
