using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace HiringCodingTestApis.Core.Models
{
    public partial class ExamGroup
    {
        public ExamGroup()
        {
            ExamSchedule = new HashSet<ExamSchedule>();
            Results = new HashSet<Results>();
            Scheduledexamdetails = new HashSet<Scheduledexamdetails>();
        }

        public int GroupId { get; set; }
        public string ExamIdJson { get; set; }
        public string GroupName { get; set; }
        public string UserId { get; set; }

        public virtual AspNetUsers User { get; set; }
        public virtual ICollection<ExamSchedule> ExamSchedule { get; set; }
        public virtual ICollection<Results> Results { get; set; }
        public virtual ICollection<Scheduledexamdetails> Scheduledexamdetails { get; set; }
    }
}
