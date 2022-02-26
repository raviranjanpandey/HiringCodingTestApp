using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace HiringCodingTestApis.Core.Models
{
    public partial class Scheduledexamdetails
    {
        public int SedId { get; set; }
        public int? ExamId { get; set; }
        public int? GroupId { get; set; }
        public int? ScheduleId { get; set; }
        public int? Questnos { get; set; }
        public int? PassingMarksPercentage { get; set; }

        public virtual ExamMaster Exam { get; set; }
        public virtual ExamGroup Group { get; set; }
        public virtual ExamSchedule Schedule { get; set; }
    }
}
