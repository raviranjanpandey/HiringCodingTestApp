using System;

namespace HiringCodingTestApis.Core.Models
{
    public partial class RefreshToken
    {
        public DateTime Expires { get; set; } = DateTime.UtcNow.AddDays(7);
        public bool IsExpired => DateTime.UtcNow >= Expires;
        public bool IActive => Revoked == null && !IsExpired;
    }
}
