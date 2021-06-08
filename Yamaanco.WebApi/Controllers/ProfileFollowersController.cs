using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Yamaanco.Application.Features.GroupFollowers.Commands;
using Yamaanco.Application.Features.GroupFollowers.Queries;
using Yamaanco.Application.Features.ProfileFollowers.Queries;

namespace Yamaanco.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProfileFollowersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProfileFollowersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create(FollowProfileCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(UnFollowProfileCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [HttpGet]
        public async Task<IActionResult> Get(GetProfileFollowersQuery query)
        {
            return Ok(await _mediator.Send(query));
        }

        [HttpGet]
        [Route("unSeenFollowersCount")]
        public async Task<IActionResult> GetUnSeenFollowersCount()
        {
            return Ok(await _mediator.Send(new GetUnSeenFollowersCountQuery()));
        }

        [HttpGet]
        [Route("profileFollowersCount/{id}")]
        public async Task<IActionResult> GetProfileFollowersCount(string id)
        {
            return Ok(await _mediator.Send(new GetProfileFollowersCountQuery(id)));
        }
    }
}