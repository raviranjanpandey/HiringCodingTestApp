namespace HiringCodingTestApis.Core.Constants
{
    public class StringConstants
    {
        public const string PasswordRegularExpressions = "(?=.*\\d)(?=.*[a-z])(?=.*[A-Z]).{4,12}$";
        public const string PasswordHint = "Password must have a number, Uppercase, Lowercase and length between 4 to 12";
    }
}
