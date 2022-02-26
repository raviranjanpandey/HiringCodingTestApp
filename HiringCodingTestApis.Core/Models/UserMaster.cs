using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace HiringCodingTestApis.Core.Models
{
    public partial class UserMaster
    {
        public UserMaster()
        {
            Answers = new HashSet<Answers>();
        }

        public int UserId { get; set; }
        public string LoginName { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public char? UserType { get; set; }

        public virtual ICollection<Answers> Answers { get; set; }
    }
}
