using HiringCodingTestApis.Core;
using HiringCodingTestApis.Core.DTO;
using HiringCodingTestApis.Core.ExamResults;
using HiringCodingTestApis.Core.Filters;
using HiringCodingTestApis.Core.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using HiringCodingTestApis.Core.Models;
using System.Security.Claims;
using System.Threading.Tasks;

namespace HiringCodingTestApis.Api.Controllers
{
    public class ExamResultController : BaseController
    {
        private readonly ExamResultService _service;
        private readonly UserService _userService;
        private readonly UserManager<AspNetUsers> _userManager;
        public ExamResultController(ExamResultService service, UserService userService, UserManager<AspNetUsers> userManager)
        {
            _service = service;
            _userService = userService;
            _userManager = userManager;
        }

        [HttpGet("get")]
        public async Task<IActionResult> Get([FromQuery] PaginationFilter pagination)
        {
            var user = await _userManager.FindByEmailAsync(User.FindFirstValue(ClaimTypes.Email));
            GetExamResultByUserId user1 = new GetExamResultByUserId ( user.Id, pagination.GetTake(), pagination.GetSkip());
            ExamResultTotalGet userId = new ExamResultTotalGet
            {
                UserId = user.Id

            };
            var result = await _service.GetExamResultUserId(user1);
            var count = await _service.ExamMasterTotalGet(userId);
            return Ok(new Response.GetAllResponse<ExamResultDto>(result.Results));
        }

        [HttpGet("getbyscheduleid")]
        public async Task<IActionResult> ExamResultGetByScheduleId([FromQuery] int ScheduleId, [FromQuery] PaginationFilter pagination)
        {
            ExamResultGetByScheduleId userid = new ExamResultGetByScheduleId
                          ( ScheduleId, pagination.GetTake(), pagination.GetSkip());
            ExamResultTotalByScheduleId userId = new ExamResultTotalByScheduleId
            {
                ScheduleId=userid.ScheduleId
            };
            var result = await _service.GetByScheduleId(userid);
            var count = await _service.ExamMasterTotalByScheduleid(userId);
            return Ok(new Response.GetAllResponse<ExamResultDto>(result.Results,count));
        }

        [HttpGet("getall")]
        public async Task<IActionResult> GetAll([FromQuery] PaginationFilter pagination)
        {
            var result = await _service.GetAll(new ExamResultGetAll(pagination.GetTake(), pagination.GetSkip()));

            return Ok(new Response.GetAllResponse<ExamResultDto>(result.Results));
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] ExamResultCreate create)
        {
            var user = await _userManager.FindByEmailAsync(User.FindFirstValue(ClaimTypes.Email));
            create.UserId = user.Id;
            var result = await _service.Create(create);            
            return Ok(result);
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] ExamResultUpdate update)
        {
            var user = await _userManager.FindByEmailAsync(User.FindFirstValue(ClaimTypes.Email));
            update.UserId = user.Id;
            var result = await _service.Update(update);           
            return Ok(result);
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete([FromQuery] ExamResultDelete delete)
        {
            var result = await _service.Delete(delete);
            return Ok(result);
        }
        [HttpGet("GetResultByUserScheduleId")]
        public async Task<IActionResult> GetAnswersByScheduleId([FromQuery]int ScheduleId)
        {
            var user = await _userManager.FindByEmailAsync(User.FindFirstValue(ClaimTypes.Email));
            GetResultByUserScheduleId user1 = new GetResultByUserScheduleId (ScheduleId,user.Id );
            var result = await _service.ResultByUserScheduleId(user1);
            return Ok(result);
        }
    }
}
