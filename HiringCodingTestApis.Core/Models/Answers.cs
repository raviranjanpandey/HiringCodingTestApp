using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace HiringCodingTestApis.Core.Models
{
    public partial class Answers
    {
        public int AnsId { get; set; }
        public string UserId { get; set; }
        public int? ExamId { get; set; }
        public int? QueId { get; set; }
        public int? AnsOption { get; set; }
        public string Answer { get; set; }
        public bool? Flag { get; set; }
        public int? ScheduleId { get; set; }

        public virtual ExamMaster Exam { get; set; }
        public virtual QuestionMaster Que { get; set; }
        public virtual ExamSchedule Schedule { get; set; }
        public virtual AspNetUsers User { get; set; }
    }
}
