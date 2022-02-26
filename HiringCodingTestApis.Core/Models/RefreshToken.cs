using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace HiringCodingTestApis.Core.Models
{
    public partial class RefreshToken
    {
        public int Id { get; set; }
        public string AppUserId { get; set; }
        public string Token { get; set; }
        public DateTime? Revoked { get; set; }

        public virtual AspNetUsers AppUser { get; set; }
    }
}
