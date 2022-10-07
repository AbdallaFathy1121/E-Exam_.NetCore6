using E_Exam.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Exam.Core.ViewModels
{
    public class LevelPageVM
    {
        public IEnumerable<LevelVM>? LstLevel { get; set; }
        public IEnumerable<TbDepartmentLevel>? LstDepartmentLevel { get; set; }
    }
}
