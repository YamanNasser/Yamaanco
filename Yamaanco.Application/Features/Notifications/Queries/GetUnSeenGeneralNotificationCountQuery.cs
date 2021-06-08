using MediatR;
using Yamaanco.Application.ApiResponses;

namespace Yamaanco.Application.Features.Notifications.Queries
{
  public  class GetUnSeenGeneralNotificationCountQuery : IRequest<Response<int>>
    {
    }
}
