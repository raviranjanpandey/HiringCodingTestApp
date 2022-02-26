using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace HiringCodingTestApis.Core.Models
{
    public partial class AspNetUsers : IdentityUser
    {
        public AspNetUsers()
        {
            Answers = new HashSet<Answers>();
            AspNetUserClaims = new HashSet<AspNetUserClaims>();
            AspNetUserLogins = new HashSet<AspNetUserLogins>();
            AspNetUserRoles = new HashSet<AspNetUserRoles>();
            AspNetUserTokens = new HashSet<AspNetUserTokens>();
            AssignedUsers = new HashSet<AssignedUsers>();
            ExamGroup = new HashSet<ExamGroup>();
            ExamMaster = new HashSet<ExamMaster>();
            ExamSchedule = new HashSet<ExamSchedule>();
            QuestionMaster = new HashSet<QuestionMaster>();
            RefreshToken = new HashSet<RefreshToken>();
            Results = new HashSet<Results>();
        }

       
        public short UserType { get; set; }
        public bool? ActiveStatus { get; set; }
        public string Name { get; set; }
        public string AllowedSchedule { get; set; }
        public string CreatedByUser { get; set; }
        public int? EntityId { get; set; }

        public virtual ICollection<Answers> Answers { get; set; }
        public virtual ICollection<AspNetUserClaims> AspNetUserClaims { get; set; }
        public virtual ICollection<AspNetUserLogins> AspNetUserLogins { get; set; }
        public virtual ICollection<AspNetUserRoles> AspNetUserRoles { get; set; }
        public virtual ICollection<AspNetUserTokens> AspNetUserTokens { get; set; }
        public virtual ICollection<AssignedUsers> AssignedUsers { get; set; }
        public virtual ICollection<ExamGroup> ExamGroup { get; set; }
        public virtual ICollection<ExamMaster> ExamMaster { get; set; }
        public virtual ICollection<ExamSchedule> ExamSchedule { get; set; }
        public virtual ICollection<QuestionMaster> QuestionMaster { get; set; }
        public virtual ICollection<RefreshToken> RefreshToken { get; set; }
        public virtual ICollection<Results> Results { get; set; }
    }
}
