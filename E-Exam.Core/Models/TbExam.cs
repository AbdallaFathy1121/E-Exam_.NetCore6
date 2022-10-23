﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Exam.Core.Models
{
    public class TbExam
    {
        public int Id { get; set; }
        public string ExamName { get; set; }
        public int SubjectId { get; set; }
        public int AccessCode { get; set; }
        public DateTime StartDateTime { get; set; }
        public int Duration { get; set; }

        // Relations
        public DbSet<TbSubject>? Subject { get; set; }
    }
}
