using System.ComponentModel.DataAnnotations;
using HiringCodingTestApis.Core.Constants;
namespace HiringCodingTestApis.Core.DTO
{
    public class ChangePasswordDto
    {
        [Required]
        [RegularExpression(StringConstants.PasswordRegularExpressions, ErrorMessage = StringConstants.PasswordHint)]
        public string CurrentPassword { get; set; }
        [Required]
        [RegularExpression(StringConstants.PasswordRegularExpressions, ErrorMessage = StringConstants.PasswordHint)]
        public string NewPassword { get; set; }
    }
}
