using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Yamaanco.Application.Features.Groups.Commands;
using Yamaanco.Application.Features.Groups.Queries;

namespace Yamaanco.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class GroupController : ControllerBase
    {
        private readonly IMediator _mediator;

        public GroupController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Create([FromForm]CreateGroupCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromForm]UpdateGroupCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest();
            }

            return Ok(await _mediator.Send(new DeleteGroupCommand() { Id = id }));
        }

        [HttpPut]
        [Route("changePhoto/{id}")]
        public async Task<IActionResult> UpdatePhoto(string id, IFormFile photo)
        {
            if (photo == null)
            {
                return BadRequest();
            }
            return Ok(await _mediator.Send(new UpdateGroupPhotoCommand()
            {
                Id = id,
                Photo = photo
            }));
        }

        [HttpGet]
        [Route("findByName")]
        public async Task<IActionResult> GetByName(GetGroupsByNameQuery query)
        {
            return Ok(await _mediator.Send(query));
        }

        [HttpGet]
        [Route("findById/{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            return Ok(await _mediator.Send(new GetGroupByIdQuery()
            {
                Id = id
            }));
        }

        [HttpGet]
        [Route("all")]
        public async Task<IActionResult> GetAll(GetGroupsQuery query)
        {
            return Ok(await _mediator.Send(query));
        }
    }
}