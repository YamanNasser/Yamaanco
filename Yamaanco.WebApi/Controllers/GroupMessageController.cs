using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Yamaanco.Application.Features.GroupMessages.Commands;
using Yamaanco.Application.Features.GroupMessages.Queries;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Yamaanco.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class GroupMessageController : Controller
    {
        private readonly IMediator _mediator;

        public GroupMessageController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreateMessageCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [HttpGet]
        public async Task<IActionResult> Get(GetMessagesQuery query)
        {
            return Ok(await _mediator.Send(query));
        }
    }
}