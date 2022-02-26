using HiringCodingTestApis.Core.Constants;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HiringCodingTestApis.Core.DTO
{
   public class CreateGetUserDto
    {
        public string UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public List<string> AllowedSchedule { get; set; }
        public string UserType { get; set; }
        public string CreatedByUser { get; set; }
        [Required]
        [RegularExpression(StringConstants.PasswordRegularExpressions, ErrorMessage = StringConstants.PasswordHint)]
        public string Password { get; set; }
        public int? EntityId { get; set; }
    }
}
