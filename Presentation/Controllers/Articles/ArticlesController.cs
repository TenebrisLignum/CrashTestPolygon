using Application.UseCases.Articles.Commands.CreateArticle;
using Application.UseCases.Articles.Commands.DeleteArticle;
using Application.UseCases.Articles.Commands.UpdateArticle;
using Application.UseCases.Articles.Queries.GetArticleById;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers.Articles
{
    [ApiController]
    [Route("api/[controller]")]
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
        public async Task<IActionResult> List([FromQuery] GetArticlesByFilterQuery query, CancellationToken cancellationToken)
        {
            var articles = await _sender.Send(query, cancellationToken);
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
