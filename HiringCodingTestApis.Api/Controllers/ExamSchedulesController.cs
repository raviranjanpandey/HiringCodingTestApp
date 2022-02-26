using HiringCodingTestApis.Core.ExamSchedules;
using HiringCodingTestApis.Core.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using HiringCodingTestApis.Core.Models;
using System.Security.Claims;
using System.Threading.Tasks;

namespace HiringCodingTestApis.Api.Controllers
{
    public class ExamSchedulesController : BaseController
    {
        private readonly ExamScheduleService _service;
        private readonly UserService _userService;
        private readonly UserManager<AspNetUsers> _userManager;
        public ExamSchedulesController(ExamScheduleService service, UserService userService, UserManager<AspNetUsers> userManager)
        {
            _service = service;
            userService = _userService;
            _userManager = userManager;
        }

        [HttpGet("get")]
        public async Task<IActionResult> Get()
        {
            var user = await _userManager.FindByEmailAsync(User.FindFirstValue(ClaimTypes.Email));
            GetExamScheduleByUserId user1 = new GetExamScheduleByUserId { UserId = user.Id };
            var result = await _service.GetExamScheduledUserId(user1);
            return Ok(result);
        }

        [HttpGet("getbyscheduleid")]
        public async Task<IActionResult> GetBySchedules([FromQuery] GetExamDetailsByScheduleId get)
        {
            var result = await _service.GetByScheduleId(get);
            return Ok(result);
        }

        [HttpGet("getbyexamgroup")]
        public async Task<IActionResult> GetByExamGroup([FromQuery] ExamScheduleGetByExamGroup get)
        {
            var result = await _service.GetByExamGroup(get);
            return Ok(result);
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] ExamScheduleCreate create)
        {
            var user = await _userManager.FindByEmailAsync(User.FindFirstValue(ClaimTypes.Email));
            create.UserId = user.Id;       
            var result = await _service.Create(create);
            return Ok(result);
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] ExamScheduleUpdate update)
        {
            var user = await _userManager.FindByEmailAsync(User.FindFirstValue(ClaimTypes.Email));
            var result = await _service.Update(update);            
            return Ok(result);
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete([FromQuery] ExamScheduleDelete delete)
        {
            var result = await _service.Delete(delete);
            return Ok(result);
        }
    }
}
