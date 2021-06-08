using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Yamaanco.Application.ApiResponses;
using Yamaanco.Application.Features.Notifications.Queries;
using Yamaanco.Application.Interfaces;
using Yamaanco.Application.Interfaces.Repositories.Notifications;

namespace Yamaanco.Application.Features.Notifications.Handlers.Queries
{
    public class GetUnSeenMessageNotificationCountHanlder : IRequestHandler<GetUnSeenMessageNotificationCountQuery, Response<int>>
    {
        private readonly INotificationsRepository _saredNotificationsCollection;
        private readonly IAccountService _accountService;

        public GetUnSeenMessageNotificationCountHanlder(INotificationsRepository saredNotificationsCollection, IAccountService accountService)
        {
            _accountService = accountService;
            _saredNotificationsCollection = saredNotificationsCollection;
        }

        public async Task<Response<int>> Handle(GetUnSeenMessageNotificationCountQuery request, CancellationToken cancellationToken)
        {
            var currentUser = _accountService.GetCurrentUser();
            var numberOfProfileNotification = await _saredNotificationsCollection
                    .GetNumberOfUnSeenMessageNotification(currentUser.Id);

            return new Response<int>(numberOfProfileNotification, "");
        }
    }
}