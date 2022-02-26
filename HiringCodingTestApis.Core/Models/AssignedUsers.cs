using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace HiringCodingTestApis.Core.Models
{
    public partial class AssignedUsers
    {
        public int AssignedUserId { get; set; }
        public string UserId { get; set; }
        public string CreatedByUser { get; set; }

        public virtual AspNetUsers User { get; set; }
    }
}
