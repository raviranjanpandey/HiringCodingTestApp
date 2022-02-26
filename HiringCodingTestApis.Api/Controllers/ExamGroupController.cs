using HiringCodingTestApis.Core.ExamGroups;
using HiringCodingTestApis.Core.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using HiringCodingTestApis.Core.Models;
using System.Security.Claims;
using System.Threading.Tasks;

namespace HiringCodingTestApis.Api.Controllers
{
    public class ExamGroupController : BaseController
    {
        private readonly ExamGroupService _service;
        private readonly UserService _userService;
        private readonly UserManager<AspNetUsers> _userManager;
        public ExamGroupController(ExamGroupService service, UserService userService, UserManager<AspNetUsers> userManager)
        {
            _service = service;
            _userService = userService;
            _userManager = userManager;
        }


        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] ExamGroupCreate create)
        {
            var user = await _userManager.FindByEmailAsync(User.FindFirstValue(ClaimTypes.Email));
            create.UserId = user.Id;
            var result = await _service.Create(create);            
            return Ok(result);
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] ExamGroupUpdate update)
        {
            var user = await _userManager.FindByEmailAsync(User.FindFirstValue(ClaimTypes.Email));
            update.UserId = user.Id;
            var result = await _service.Update(update);            
            return Ok(result);
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete([FromQuery] ExamGroupDelete delete)
        {
            var result = await _service.Delete(delete);
            return Ok(result);
        }

        [HttpGet("get")]
        public async Task<IActionResult> Get()
        {
            var user = await _userManager.FindByEmailAsync(User.FindFirstValue(ClaimTypes.Email));
            ExamGroupGet user1 = new ExamGroupGet { UserId = user.Id };
            var result = await _service.Get(user1);
            return Ok(result);
        }

        [HttpGet("getbyid")]
        public async Task<IActionResult> GetById([FromQuery] ExamGroupGetById getById)
        {
            var result = await _service.GetById(getById);
            return Ok(result);
        }

        [HttpGet("getbyname")]
        public async Task<IActionResult> GetByName([FromQuery] ExamGroupGetByName getByName)
        {
            var result = await _service.GetByName(getByName);
            return Ok(result);
        }
    }
}
