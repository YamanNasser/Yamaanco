using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Yamaanco.Application.Features.Notifications.Queries;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Yamaanco.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class NotificationsController : Controller
    {
        private readonly IMediator _mediator;

        public NotificationsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("allMessageNotification")]
        public async Task<IActionResult> GetAllMessageNotification(GetAllMessageNotificationListQuery query)
        {
            return Ok(await _mediator.Send(query));
        }

        [HttpGet]
        [Route("unSeenMessageNotifications")]
        public async Task<IActionResult> GetUnSeenMessageNotifications(GetUnSeenMessageNotificationListQuery query)
        {
            return Ok(await _mediator.Send(query));
        }

        [HttpGet]
        [Route("unSeenMessageNotificationsCount")]
        public async Task<IActionResult> GetUnSeenMessageNotificationsCount()
        {
            return Ok(await _mediator.Send(new GetUnSeenMessageNotificationCountQuery()));
        }

        [HttpGet]
        [Route("allGeneralNotifications")]
        public async Task<IActionResult> GetAllGeneralNotifications(GetAllGeneralNotificationListQuery query)
        {
            return Ok(await _mediator.Send(query));
        }

        [HttpGet]
        [Route("unSeenGeneralNotifications")]
        public async Task<IActionResult> GetUnSeenGeneralNotifications(GetUnSeenGeneralNotificationListQuery query)
        {
            return Ok(await _mediator.Send(query));
        }

        [HttpGet]
        [Route("unSeenGeneralNotificationCount")]
        public async Task<IActionResult> GetUnSeenGeneralNotificationCount()
        {
            return Ok(await _mediator.Send(new GetUnSeenGeneralNotificationCountQuery()));
        }
    }
}