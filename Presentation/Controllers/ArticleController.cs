using Application.Logic.Articles.Commands.CreateArticle;
using Application.Logic.Articles.Queries.GetArticleById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticleController : ControllerBase
    {
        private readonly ISender _sender;

        public ArticleController(ISender sender)
        {
            _sender = sender;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetArticleByFilterQuery command)
        {
            var article = await _sender.Send(command);
            return Ok(article);
        }

        [HttpGet("table")]
        public async Task<IActionResult> Table([FromQuery] GetArticleByFilterQuery command)
        {
            var article = await _sender.Send(command);
            return Ok(article);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateArticleCommand command)
        {
            var articleId = await _sender.Send(command);
            return Ok(articleId);
        }
    }
}
