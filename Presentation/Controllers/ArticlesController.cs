using Application.Logic.Articles.Commands.CreateArticle;
using Application.Logic.Articles.Commands.DeleteArticle;
using Application.Logic.Articles.Commands.UpdateArticle;
using Application.Logic.Articles.Queries.GetArticleById;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = Consts.AdminRoleString)]
    public class ArticlesController : ControllerBase
    {
        private readonly ISender _sender;

        public ArticlesController(ISender sender)
        {
            _sender = sender;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Get([FromQuery] GetArticleByIdQuery query)
        {
            var article = await _sender.Send(query);
            return Ok(article);
        }

        [HttpGet("list")]
        [AllowAnonymous]
        public async Task<IActionResult> List([FromQuery] GetArticleByFilterQuery command, CancellationToken cancellationToken)
        {
            var articles = await _sender.Send(command, cancellationToken);
            return Ok(articles);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateArticleCommand command)
        {
            var articleId = await _sender.Send(command);
            return Ok(articleId);
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateArticleCommand command)
        {
            var articleId = await _sender.Send(command);
            return Ok(articleId);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] DeleteArticleCommand command)
        {
            var articleId = await _sender.Send(command);
            return Ok(articleId);
        }
    }
}
