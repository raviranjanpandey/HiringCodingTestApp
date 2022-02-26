using HiringCodingTestApis.Core.ScheduledExamDetail;
using HiringCodingTestApis.Core.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HiringCodingTestApis.Api.Controllers
{
    public class ScheduledExamDetailsController : BaseController
    {
        private readonly SchExamDetService _service;
        public ScheduledExamDetailsController(SchExamDetService service)
        {
            _service = service;
        }

        [HttpGet("get")]
        public async Task<IActionResult> Get([FromQuery] SchExamDetGet get)
        {
            var result = await _service.Get(get);
            return Ok(result);
        }

        [HttpGet("getbyscheduleid")]
        public async Task<IActionResult> GetBySchedules([FromQuery] SchExamDetGetByScheduleId get)
        {
            var result = await _service.GetByScheduleId(get);
            return Ok(result);
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] SchExamDetCreate create)
        {
            var result = await _service.Create(create);
            return Ok(result);
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete([FromQuery] SchExamDetDelete delete)
        {
            var result = await _service.Delete(delete);
            return Ok(result);
        }
    }
}
