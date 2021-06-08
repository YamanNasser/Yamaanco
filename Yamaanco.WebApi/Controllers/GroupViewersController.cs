using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Yamaanco.Application.Features.GroupViewers.Commands;
using Yamaanco.Application.Features.GroupViewers.Queries;

namespace Yamaanco.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class GroupViewersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public GroupViewersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> Create(string id)
        {
            return Ok(await _mediator.Send(new ViewGroupCommand(id)));
        }

        [HttpGet]
        public async Task<IActionResult> Get(GetGroupViewersQuery query)
        {
            return Ok(await _mediator.Send(query));
        }
    }
}