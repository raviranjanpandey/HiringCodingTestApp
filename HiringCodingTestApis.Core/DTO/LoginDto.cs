using HiringCodingTestApis.Core.Constants;
using System.ComponentModel.DataAnnotations;

namespace HiringCodingTestApis.Core.DTO
{
    public class LoginDto
    {
        [Required]
        [EmailValidationAttributes(ErrorMessage = "Invalid Email format.")]
        public string Email { get; set; }
        [Required]
        [RegularExpression(StringConstants.PasswordRegularExpressions, ErrorMessage = StringConstants.PasswordHint)]
        public string Password { get; set; }
    }
}
