using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace HiringCodingTestApis.Core.Models
{
    public partial class QuestionMaster
    {
        public QuestionMaster()
        {
            Answers = new HashSet<Answers>();
        }

        public int QueId { get; set; }
        public int? ExamId { get; set; }
        public string SubModule { get; set; }
        public string Question { get; set; }
        public short? AnswerType { get; set; }
        public string Option1 { get; set; }
        public string Option2 { get; set; }
        public string Option3 { get; set; }
        public string Option4 { get; set; }
        public short? CorrectOption { get; set; }
        public short? Seconds { get; set; }
        public int? GroupId { get; set; }
        public string UserId { get; set; }

        public virtual ExamMaster Exam { get; set; }
        public virtual AspNetUsers User { get; set; }
        public virtual ICollection<Answers> Answers { get; set; }
    }
}
