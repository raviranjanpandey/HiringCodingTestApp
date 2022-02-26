using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace HiringCodingTestApis.Core.Models
{
    public partial class ExamMaster
    {
        public ExamMaster()
        {
            Answers = new HashSet<Answers>();
            QuestionMaster = new HashSet<QuestionMaster>();
            Results = new HashSet<Results>();
            Scheduledexamdetails = new HashSet<Scheduledexamdetails>();
        }

        public int ExamId { get; set; }
        public string ExamName { get; set; }
        public string Description { get; set; }
        public string UserId { get; set; }

        public virtual AspNetUsers User { get; set; }
        public virtual ICollection<Answers> Answers { get; set; }
        public virtual ICollection<QuestionMaster> QuestionMaster { get; set; }
        public virtual ICollection<Results> Results { get; set; }
        public virtual ICollection<Scheduledexamdetails> Scheduledexamdetails { get; set; }
    }
}
