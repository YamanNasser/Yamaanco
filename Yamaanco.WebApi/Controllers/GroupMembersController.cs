using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Yamaanco.Application.Features.GroupFollowers.Commands;
using Yamaanco.Application.Features.GroupMembers.Commands;
using Yamaanco.Application.Features.GroupMembers.Queries;

namespace Yamaanco.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class GroupMembersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public GroupMembersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route("sendEmailRequestToNewMember")]
        public async Task<IActionResult> SendEmailRequestToNewMember(SendEmailRequestToNewMemberRequest request)
        {
            var origin = Request.Headers["origin"];

            return Ok(await _mediator.Send(
                new SendRequestToNewMemberCommand(request.GroupId,
                origin, request.InvitedEmail)));
        }

        [HttpPost("{groupId}")]
        public async Task<IActionResult> FollowGroup(string groupId)
        {
            return Ok(await _mediator.Send(new FollowGroupCommand(groupId)));
        }

        [HttpPost()]
        [Route("addNewMember")]
        public async Task<IActionResult> AddNewMember(AddNewMemberCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [HttpDelete("{groupId}")]
        public async Task<IActionResult> UnFollowGroup(string groupId)
        {
            return Ok(await _mediator.Send(new UnFollowGroupCommand(groupId)));
        }

        [HttpGet]
        [Route("accept-join/{code}")]
        public async Task<IActionResult> AcceptJoinGroup(string code)
        {
            return Ok(await _mediator.Send(new AcceptJoinGroupCommand(code)));
        }

        [HttpGet]
        public async Task<IActionResult> Get(GetGroupMemberListQuery query)
        {
            return Ok(await _mediator.Send(query));
        }
    }
}