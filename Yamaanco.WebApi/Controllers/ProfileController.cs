using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Yamaanco.Application.Features.Profiles.Commands;
using Yamaanco.Application.Features.Profiles.Queries;

namespace Yamaanco.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProfileController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProfileController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Create(CreateProfileCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateProfileCommand command)
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

            return Ok(await _mediator.Send(new DeleteProfileCommand() { Id = id }));
        }

        [HttpPut]
        [Route("changePhoto/{id}")]
        public async Task<IActionResult> UpdatePhoto(string id, IFormFile photo)
        {
            if (photo == null)
            {
                return BadRequest();
            }
            return Ok(await _mediator.Send(new UpdateProfilePhotoCommand()
            {
                Id = id,
                Photo = photo
            }));
        }

        [HttpGet]
        [Route("findByName")]
        public async Task<IActionResult> GetByName(GetProfileByNameQuery query)
        {
            return Ok(await _mediator.Send(query));
        }

        [HttpGet]
        [Route("findById")]
        public async Task<IActionResult> GetById(string id)
        {
            return Ok(await _mediator.Send(new GetProfileByIdQuery()
            {
                Id = id
            }));
        }

        [HttpGet]
        [Route("all")]
        public async Task<IActionResult> GetAll(GetProfileQuery query)
        {
            return Ok(await _mediator.Send(query));
        }
    }
}