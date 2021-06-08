using MediatR;
using Yamaanco.Application.ApiResponses;

namespace Yamaanco.Application.Features.Notifications.Queries
{
   public class GetUnSeenMessageNotificationCountQuery : IRequest<Response<int>>
    {
    }
}
