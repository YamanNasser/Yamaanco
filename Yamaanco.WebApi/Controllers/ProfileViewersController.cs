using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Yamaanco.Application.Features.ProfileViewers.Commands;
using Yamaanco.Application.Features.ProfileViewers.Queries;

namespace Yamaanco.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProfileViewersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProfileViewersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> Create(string id)
        {
            return Ok(await _mediator.Send(new ViewProfileCommand(id)));
        }

        [HttpGet]
        public async Task<IActionResult> Get(GetProfileViewersQuery query)
        {
            return Ok(await _mediator.Send(query));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCount(string id)
        {
            return Ok(await _mediator.Send(new GetProfileViewersCountQuery(id)));
        }
    }
}