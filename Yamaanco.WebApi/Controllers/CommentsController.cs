using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Yamaanco.Application.Features.Comments.Commands;
using Yamaanco.Application.Features.Comments.Queries;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Yamaanco.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CommentsController : Controller
    {
        private readonly IMediator _mediator;

        public CommentsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreateCommentCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromForm] UpdateCommentCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            return Ok(await _mediator.Send(new DeleteCommentCommand(id)));
        }

        [HttpGet]
        public async Task<IActionResult> Get(GetCommentsQuery query)
        {
            return Ok(await _mediator.Send(query));
        }

        [HttpGet]
        [Route("find")]
        public async Task<IActionResult> Get(FindCommentsQuery query)
        {
            return Ok(await _mediator.Send(query));
        }

        [HttpGet]
        [Route("findByHashtag")]
        public async Task<IActionResult> GetCommentsByHashtag(FindCommentsByHashtagsQuery query)
        {
            return Ok(await _mediator.Send(query));
        }
         
        [HttpGet]
        [Route("mentionsList")]
        public async Task<IActionResult> GetCommentMentionsList(GetCommentMentionsListQuery query)
        {
            return Ok(await _mediator.Send(query));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetComment(string id)
        {
            return Ok(await _mediator.Send(new GetSpecificCommentQuery(id)));
        }

        [HttpPut]
        [Route("upvote/{id}")]
        public async Task<IActionResult> Upvote(string id)
        {
            return Ok(await _mediator.Send(new UpdateCommentUpvoteCommand(id)));
        }
    }
}