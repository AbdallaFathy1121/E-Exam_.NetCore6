using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Exam.Core.Models
{
    public class TbQuestion
    {
        public int Id { get; set; }
        public int ModelId { get; set; }
        public string QuestionName { get; set; }
        public string Q1 { get; set; }
        public string Q2 { get; set; }
        public string? Q3 { get; set; }
        public string? Q4 { get; set; }
        public string CorrectAnswer { get; set; }


        // Relations
        public DbSet<TbModel>? Model { get; set; }
    }
}
