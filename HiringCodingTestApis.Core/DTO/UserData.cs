using System;

namespace HiringCodingTestApis.Core.DTO
{
    public class UserData
    {
        public string UserId { get; set; }
        public string Token { get; set; }
        public string UserName { get; set; }
        public short UserType { get; set; }
        public string Name { get; set; }
        public string AllowedSchedule { get; set; }
        public int? EntityId { get; set; }

    }
}
