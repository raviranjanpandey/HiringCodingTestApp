using HiringCodingTestApis.Core;
using HiringCodingTestApis.Core.DTO;
using HiringCodingTestApis.Core.Filters;
using HiringCodingTestApis.Core.QuestionsMaster;
using HiringCodingTestApis.Core.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using HiringCodingTestApis.Core.Models;
using System.Security.Claims;
using System.Threading.Tasks;

namespace HiringCodingTestApis.Api.Controllers
{
    public class QuestionMasterController : BaseController
    {
        private readonly QuestionMasterService _questionMasterService;
        private readonly UserService _userService;
        private readonly UserManager<AspNetUsers> _userManager;
        public QuestionMasterController(QuestionMasterService questionMasterService, UserService userService, UserManager<AspNetUsers> userManager)
        {
            _questionMasterService = questionMasterService;
            _userService = userService;
            _userManager = userManager;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] QuestionMastersCreate create)
        {
            var user = await _userManager.FindByEmailAsync(User.FindFirstValue(ClaimTypes.Email));
            create.UserId = user.Id;
            var result = await _questionMasterService.Create(create);            
            return Ok(result);
        }

        [HttpPost("createrange")]
        public async Task<IActionResult> CreateRange([FromBody] QuestionMasterCreateRangeCommand create)
        {
            var result = await _questionMasterService.CreateRange(create);
            return Ok(result);
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] QuestionMastersUpdate update)
        {
            var user = await _userManager.FindByEmailAsync(User.FindFirstValue(ClaimTypes.Email));
            //update.UserId = user.Id;
            var result = await _questionMasterService.Update(update);
            return Ok(result);
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete([FromQuery] QuestionMastersDelete delete)
        {
            var result = await _questionMasterService.Delete(delete);
            return Ok(result);
        }

        [HttpGet("getbyexamid")]
        public async Task<IActionResult> GetByExamId([FromQuery] int ExamId, string UserId, [FromQuery] PaginationFilter pagination)
        {
            var result = await _questionMasterService.GetByExamId(ExamId,UserId,pagination.GetTake(),pagination.GetSkip());
            var count = await _questionMasterService.totalQuestionMasterByExamId(ExamId, UserId);
            return Ok(new Response.GetAllResponse<QuestionMastersDto>(result.QuestionsList, count));
        }

        [HttpGet("get")]
        public async Task<IActionResult> Get([FromQuery] PaginationFilter pagination)
        {
            var user = await _userManager.FindByEmailAsync(User.FindFirstValue(ClaimTypes.Email));
            QuestionMasterByGet user1 = new QuestionMasterByGet ( user.Id,pagination.GetTake(),pagination.GetSkip() );
            QuestionMasterTotal userid = new QuestionMasterTotal
            {
                UserId = user.Id

            };                
            var result = await _questionMasterService.GetByUserId(user1);
            var count = await _questionMasterService.QuestionMasterTotal(userid);
            return Ok(new Response.GetAllResponse<QuestionMastersDto>(result.QuestionsList, count));      
        }

        [HttpGet("getFilter")]
        public async Task<IActionResult> QuestionFilter([FromQuery] int examId, string serachvalue, [FromQuery] PaginationFilter pagination)
        {            
            var result = await _questionMasterService.QuestionMasterFilter(examId,serachvalue , pagination.GetTake(), pagination.GetSkip());
            var count = await _questionMasterService.totalFilter(examId,serachvalue);
            return Ok(new Response.GetAllResponse<QuestionMastersDto>(result.QuestionsList,count));
        }
    }
}
