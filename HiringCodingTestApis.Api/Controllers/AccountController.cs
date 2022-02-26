using HiringCodingTestApis.Core.Constants;
using HiringCodingTestApis.Core.DTO;
using HiringCodingTestApis.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using HiringCodingTestApis.Core.Models;
using System;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace HiringCodingTestApis.Api.Controllers
{
    public class AccountController : BaseController
    {
        private readonly UserManager<AspNetUsers> _userManager;
        private readonly SignInManager<AspNetUsers> _signInManager;
        private readonly TokenService _tokenService;
        public AccountController(UserManager<AspNetUsers> userManager,
            SignInManager<AspNetUsers> signInManager,
            TokenService tokenService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);

            if (user == null) return Unauthorized();

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

            if (result.Succeeded)
            {
                await SetRefreshToken(user);
                return Ok(CreateUserObject(user));
            }

            return Unauthorized("User does not exist. Check valid email and password.");
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            if (await _userManager.Users.AnyAsync(x => x.Email == registerDto.Email))
            {
                return BadRequest("User already registered.");
            }

            var user = new AspNetUsers
            {
                Email = registerDto.Email,
                Name = registerDto.Name,
                UserName = registerDto.UserName,
                UserType = registerDto.UserType,
                PhoneNumber = registerDto.PhoneNumber
            };

            var result = await _userManager.CreateAsync(user, registerDto.Password);

            if (result.Succeeded)
            {
                return Ok($"Registered Successfully.");
            }

            return BadRequest("Problem registering user.");
        }

        [Authorize]
        [HttpPost("refreshToken")]
        public async Task<IActionResult> RefreshToken()
        {
            var refreshToken = Request.Cookies["RefreshToken"];
            var user = await _userManager.Users.Include(r => r.RefreshToken)
                .FirstOrDefaultAsync(x => x.UserName == User.FindFirstValue(ClaimTypes.Name));

            if (user == null) return Unauthorized("User doest not exist.");

            var oldToken = user.RefreshToken.SingleOrDefault(x => x.Token == refreshToken);

            if (oldToken != null && !oldToken.IActive) return Unauthorized("Refresh Token is expired.");

            return Ok(CreateUserObject(user));
        }

        [AllowAnonymous]
        [HttpGet("forgotpassword")]
        public async Task<IActionResult> ForgotPassword(string email)
        {
            if (!EmailValidator.IsValidEmail(email)) return BadRequest("Invalid Email format.");

            var user = await _userManager.FindByEmailAsync(email);

            if (user == null) return Unauthorized("User doest not exist.");

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            token = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));

            return Ok($"Please Reset the password with the following token: {token}");
        }

        [AllowAnonymous]
        [HttpPost("resetpassword")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto resetPassword)
        {
            if (!EmailValidator.IsValidEmail(resetPassword.email)) return BadRequest("Invalid Email format.");

            var user = await _userManager.FindByEmailAsync(resetPassword.email);

            if (user == null) return Unauthorized();

            var decodedTokenBytes = WebEncoders.Base64UrlDecode(resetPassword.token);

            var decodedToken = Encoding.UTF8.GetString(decodedTokenBytes);

            var result = await _userManager.ResetPasswordAsync(user, decodedToken, resetPassword.newpassword);

            if (result.Succeeded)
            {
                return Ok($"Password successfully updated.");
            }

            return BadRequest("Could not reset password.");
        }

        [Authorize]
        [HttpDelete("deleteuser")]
        public async Task<IActionResult> Delete(string idToDelete)
        {
            var admin = await _userManager.FindByEmailAsync(User.FindFirstValue(ClaimTypes.Email));

            if (admin.UserType != (short)UserTypes.Admin) return BadRequest("Only Admin can delete the user.");

            var user = await _userManager.FindByIdAsync(idToDelete);

            if (user == null) return Unauthorized("User doest not exist.");

            if (idToDelete == admin.Id) return BadRequest("Admin can not be deleted.");

            var result = await _userManager.DeleteAsync(user);

            return Ok(result.Succeeded);
        }

        private async Task SetRefreshToken(AspNetUsers user)
        {
            var refreshToken = _tokenService.GenerateRefreshToken();
            user.RefreshToken.Add(refreshToken);
            await _userManager.UpdateAsync(user);

            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.UtcNow.AddDays(7)
            };

            Response.Cookies.Append("RefreshToken", refreshToken.Token, cookieOptions);
        }

        private UserData CreateUserObject(AspNetUsers user)
        {
            return new UserData
            {
                UserId = user.Id,
                Token = _tokenService.CreateToken(user),
                UserName = user.UserName,
                UserType = user.UserType,
                Name = user.Name,
                AllowedSchedule = user.AllowedSchedule,
                EntityId = user.EntityId
            };
        }
    }
}
