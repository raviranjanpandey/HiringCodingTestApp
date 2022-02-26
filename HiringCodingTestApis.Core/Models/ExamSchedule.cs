using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace HiringCodingTestApis.Core.Models
{
    public partial class ExamSchedule
    {
        public ExamSchedule()
        {
            Answers = new HashSet<Answers>();
            Results = new HashSet<Results>();
            Scheduledexamdetails = new HashSet<Scheduledexamdetails>();
        }

        public int ScheduleId { get; set; }
        public int? GroupId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool? Active { get; set; }
        public int? NumOfQuestions { get; set; }
        public string UserId { get; set; }
        public int? TestDuration { get; set; }
        public bool? Isresult { get; set; }
        public bool? Isanswer { get; set; }
        public bool? Isquestimer { get; set; }

        public virtual ExamGroup Group { get; set; }
        public virtual AspNetUsers User { get; set; }
        public virtual ICollection<Answers> Answers { get; set; }
        public virtual ICollection<Results> Results { get; set; }
        public virtual ICollection<Scheduledexamdetails> Scheduledexamdetails { get; set; }
    }
}
