using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers.Chat
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ChatRoomController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get(string name)
        {
            return Ok();
        }

        [HttpPost]
        public IActionResult Create()
        {
            return Ok();
        }
    }
}
