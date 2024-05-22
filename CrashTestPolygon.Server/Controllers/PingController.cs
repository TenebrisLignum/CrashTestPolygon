using Microsoft.AspNetCore.Mvc;

namespace CrashTestPolygon.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PingController : ControllerBase
    {
        [HttpGet]
        public IActionResult Ping()
        {
            var response = "Nice!";
            return Ok(response);
        }
    }
}
