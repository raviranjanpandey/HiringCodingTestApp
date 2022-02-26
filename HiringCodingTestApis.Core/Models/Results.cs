using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace HiringCodingTestApis.Core.Models
{
    public partial class Results
    {
        public int ResId { get; set; }
        public string UserId { get; set; }
        public int? ExamId { get; set; }
        public int? GroupId { get; set; }
        public int? CorrectAnswer { get; set; }
        public int? WrongAnswer { get; set; }
        public int? SkippedAnswer { get; set; }
        public int? FinalResult { get; set; }
        public bool? Flag { get; set; }
        public int? ScheduleId { get; set; }

        public virtual ExamMaster Exam { get; set; }
        public virtual ExamGroup Group { get; set; }
        public virtual ExamSchedule Schedule { get; set; }
        public virtual AspNetUsers User { get; set; }
    }
}
