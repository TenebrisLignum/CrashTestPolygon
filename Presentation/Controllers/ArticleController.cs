using Application.Logic.Articles.Commands.CreateArticle;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticleController : ControllerBase
    {
        private readonly ISender sender;

        public ArticleController(ISender sender)
        {
            this.sender = sender;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateArticleCommand command)
        {
            var productId = await sender.Send(command);
            return Ok(productId);
        }
    }
}
