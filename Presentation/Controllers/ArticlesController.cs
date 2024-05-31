using Application.Logic.Articles.Commands.CreateArticle;
using Application.Logic.Articles.Queries.GetArticleById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticlesController : ControllerBase
    {
        private readonly ISender _sender;

        public ArticlesController(ISender sender)
        {
            _sender = sender;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetArticleByIdQuery command)
        {
            var article = await _sender.Send(command);
            return Ok(article);
        }

        [HttpGet("list")]
        public async Task<IActionResult> List([FromQuery] GetArticleByFilterQuery command)
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
