using System;
using System.Collections.Generic;
using System.Text;

namespace HiringCodingTestApis.Core.DTO
{
   public class CreateUserByUserIdDto
    {
        public string UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string AllowedSchedule { get; set; }
        public string UserType { get; set; }
        public string CreatedByUser { get; set; }       
        public string Password { get; set; }
        public int? EntityId { get; set; }
    }
}
