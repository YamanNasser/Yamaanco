using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Yamaanco.Application.Features.ProfileComments.Commands;
using Yamaanco.Application.Features.ProfileComments.Queries;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Yamaanco.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProfileCommentsController : Controller
    {
        private readonly IMediator _mediator;

        public ProfileCommentsController(IMediator mediator)
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

        [HttpPut]
        [Route("upvote/{id}")]
        public async Task<IActionResult> Upvote(string id)
        {
            return Ok(await _mediator.Send(new UpdateCommentUpvoteCommand(id)));
        }
    }
}