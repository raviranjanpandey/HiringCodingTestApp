using HiringCodingTestApis.Core.Answer;
using HiringCodingTestApis.Core.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using HiringCodingTestApis.Core.Models;
using System.Security.Claims;
using System.Threading.Tasks;

namespace HiringCodingTestApis.Api.Controllers
{
    public class AnswersController : BaseController
    {
        private readonly AnswerService _service;
        private readonly UserService _userService;
        private readonly UserManager<AspNetUsers> _userManager;
        public AnswersController(AnswerService service, UserService userService, UserManager<AspNetUsers> userManager)
        {
            _service = service;
            _userService = userService;
            _userManager = userManager;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] AnswerCreate create)
        {
            var created = await _service.Create(create);
            return Ok(created);
        }

        [HttpPost("createrange")]
        public async Task<IActionResult> CreateRange([FromBody] AnswerCreateRangeCommand create)
        {
            var user = await _userManager.FindByEmailAsync(User.FindFirstValue(ClaimTypes.Email));
            foreach(var item in create.AnswerList)
            {
                item.UserId = user.Id;
            }
            var created = await _service.CreateRange(create);
            return Ok(created);
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] AnswerUpdate update)
        {
            var created = await _service.Update(update);
            return Ok(created);
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete([FromQuery] AnswersDelete delete)
        {
            var result = await _service.Delete(delete);
            return Ok(result);
        }

        [HttpDelete("deletebygroupid")]
        public async Task<IActionResult> DeleteByGroupID([FromQuery] DeleteByExamGroupId delete)
        {
            var result = await _service.DeleteByGroupID(delete);
            return Ok(result);
        }

        [HttpDelete("deletebyuserid")]
        public async Task<IActionResult> DeleteByUserID([FromQuery] AnswerDeleteByUserId delete)
        {
            var result = await _service.DeleteByUserID(delete);
            return Ok(result);
        }

        [HttpGet("getbyexamgroupid")]
        public async Task<IActionResult> GetAnswersByExamGroupId([FromQuery] GetAnswersByExamGroupId get)
        {
            var result = await _service.GetAnswersByExamGroupId(get);
            return Ok(result);
        }

        [HttpGet("getbyexamid")]
        public async Task<IActionResult> GetAnswersByExamId([FromQuery] GetAnswersByExamId get)
        {
            var result = await _service.GetAnswersByExamId(get);
            return Ok(result);
        }

        [HttpGet("getbyuserid")]
        public async Task<IActionResult> GetAnswersByUserID()
        {
            var user = await _userManager.FindByEmailAsync(User.FindFirstValue(ClaimTypes.Email));
            GetAnswersByUserId user1 = new GetAnswersByUserId { UserId = user.Id};
            var result = await _service.GetAnswersByUserId(user1);
            return Ok(result);
        }
        [HttpGet("getAnswersByScheduleId")]
        public async Task<IActionResult> GetAnswersByScheduleId([FromQuery] int ScheduleId)
        {
            var user = await _userManager.FindByEmailAsync(User.FindFirstValue(ClaimTypes.Email));
            AnswerByScheduleId user1 = new AnswerByScheduleId (ScheduleId, user.Id);
            var result = await _service.GetAnswerByScheduleId(user1);
            return Ok(result);
        }
        [HttpGet("getAnswersByQueId")]
        public async Task<IActionResult> GetAnswersByQueId([FromQuery] GetAnswerByQueId get)
        {
            var result = await _service.GetAnswerByQueId(get);
            return Ok(result);
        }
    }
}
