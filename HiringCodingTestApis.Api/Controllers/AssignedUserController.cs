using HiringCodingTestApis.Core.AssignedUser;
using HiringCodingTestApis.Core.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using HiringCodingTestApis.Core.Models;
using System.Security.Claims;
using System.Threading.Tasks;

namespace HiringCodingTestApis.Api.Controllers
{
    public class AssignedUserController : BaseController
    {
        private readonly AssignedUserService _service;
        private readonly UserService _userService;
        private readonly UserManager<AspNetUsers> _userManager;
        public AssignedUserController(AssignedUserService service, UserService userService, UserManager<AspNetUsers> userManager)
        {
            _service = service;
            _userService = userService;
            _userManager = userManager;
        }


        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] AssignedUserCreate create)
        {

            var result = await _service.Create(create);
            return Ok(result);
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] AssignedUserUpdate update)
        {

            var result = await _service.Update(update);
            return Ok(result);
        }
        [HttpGet("get")]
        public async Task<IActionResult> Get()
        {
            var user = await _userManager.FindByEmailAsync(User.FindFirstValue(ClaimTypes.Email));
            AssignedUserGet user1 = new AssignedUserGet { CreatedByUser = user.Id };
            var result = await _service.Get(user1);
            return Ok(result);
        }
    }
}
