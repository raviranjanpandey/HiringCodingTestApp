using HiringCodingTestApis.Core.DTO;
using HiringCodingTestApis.Core.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using HiringCodingTestApis.Core.Models;
using System.Security.Claims;
using System.Threading.Tasks;
using HiringCodingTestApis.Core.Constants;

namespace HiringCodingTestApis.Api.Controllers
{
    public class UsersController : BaseController
    {
        private readonly UserService _userService;
        private readonly UserManager<AspNetUsers> _userManager;

        public UsersController(UserService userService, UserManager<AspNetUsers> userManager)
        {
            _userService = userService;
            _userManager = userManager;
        }

        [HttpGet("getusers")]
        public async Task<IActionResult> GetUsers()
        {
            var admin = await _userManager.FindByEmailAsync(User.FindFirstValue(ClaimTypes.Email));

            if (admin.UserType != (short)UserTypes.Admin) return BadRequest("Only Admin can get the users.");

            return Ok(await _userService.GetUsers());
        }
    }
}
