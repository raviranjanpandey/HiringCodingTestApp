using Microsoft.AspNetCore.Identity;
using HiringCodingTestApis.Core.Models;
using System;

namespace HiringCodingTestApis.Core.Constants
{
    public class Helper
    {
        public static string GenerateHashedPassword(AspNetUsers users)
        {
            string pwd = "S007@";
            string str = pwd + users.Email.Substring(0, 4);

            var hasher = new Microsoft.AspNetCore.Identity.PasswordHasher<IdentityUser>();
            IdentityUser identityUser = new IdentityUser(users.UserName);

            return hasher.HashPassword(identityUser, str);
        }
    }
}
