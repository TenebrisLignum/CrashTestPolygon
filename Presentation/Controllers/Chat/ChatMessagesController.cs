using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers.Chat
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ChatMessages : ControllerBase
    {
        [HttpPost("/send")]
        public async Task<IActionResult> Send()
        {
            return Ok();
        }
    }
}
