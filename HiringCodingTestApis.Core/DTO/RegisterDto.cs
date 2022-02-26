using HiringCodingTestApis.Core.Constants;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HiringCodingTestApis.Core.DTO
{
    public class RegisterDto
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        [EmailValidationAttributes(ErrorMessage = "Invalid Email format.")]
        public string Email { get; set; }
        [Required]
        public short UserType { get; set; }
        [Required]
        [RegularExpression(StringConstants.PasswordRegularExpressions, ErrorMessage = StringConstants.PasswordHint)]
        public string Password { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }

        public static KeyValuePair<string, string> GetPassword(string email)
        {
            string passwordInitials = email.Split('@')[0].Substring(0, 4).ToLower();
            //int number = new Random().Next(1, 10);
            int number = 7;
            return new KeyValuePair<string, string>(email, $"{passwordInitials}@{passwordInitials.Substring(0, 1).ToUpper()}{number}");
        }
    }
}
