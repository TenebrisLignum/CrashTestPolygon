using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers.Chat
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ChatMessages : ControllerBase
    {
        public IActionResult Create()
        {
            return Ok();
        }
    }
}
