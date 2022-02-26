using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace HiringCodingTestApis.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [EnableCors("CorsPolicy")]
    public class BaseController : ControllerBase
    {
    }
}
