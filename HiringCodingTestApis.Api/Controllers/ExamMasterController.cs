using HiringCodingTestApis.Core.ExamsMaster;
using HiringCodingTestApis.Core.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using HiringCodingTestApis.Core.Models;
using System.Security.Claims;
using System.Threading.Tasks;

namespace HiringCodingTestApis.Api.Controllers
{
    public class ExamMasterController : BaseController
    {
        private readonly ExamMasterService _examMasterService;
        private readonly UserService _userService;
        private readonly UserManager<AspNetUsers> _userManager;
        public ExamMasterController(ExamMasterService service, UserService userService, UserManager<AspNetUsers> userManager)
        {
            _examMasterService = service;
            _userService = userService;
            _userManager = userManager;
        }
        [HttpGet("get")]
        public async Task<IActionResult> Get()
        {
            var user = await _userManager.FindByEmailAsync(User.FindFirstValue(ClaimTypes.Email));
            ExamMasterGet user1 = new ExamMasterGet { UserId = user.Id };
            var result = await _examMasterService.Get(user1);
            return Ok(result);
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] ExamMasterCreate create)
        {
            var user = await _userManager.FindByEmailAsync(User.FindFirstValue(ClaimTypes.Email));
            create.UserId = user.Id;
            var result = await _examMasterService.Create(create);
            return Ok(result);
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] ExamMasterUpdate update)
        {
            var user = await _userManager.FindByEmailAsync(User.FindFirstValue(ClaimTypes.Email));
            update.UserId = user.Id;
            var result = await _examMasterService.Update(update);
            return Ok(result);
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete([FromQuery] ExamMasterDelete delete)
        {
            var result = await _examMasterService.Delete(delete);
            return Ok(result);
        }
    }
}
