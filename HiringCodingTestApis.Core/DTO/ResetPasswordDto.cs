namespace HiringCodingTestApis.Core.DTO
{
    public class ResetPasswordDto
    {
        public string newpassword { get; set; }
        public string email { get; set; }
        public string token { get; set; }
    }
}
