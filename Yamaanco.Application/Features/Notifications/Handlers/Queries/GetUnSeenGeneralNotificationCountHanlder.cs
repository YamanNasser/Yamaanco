using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Yamaanco.Application.ApiResponses;
using Yamaanco.Application.Features.Notifications.Queries;
using Yamaanco.Application.Interfaces;
using Yamaanco.Application.Interfaces.Repositories.Notifications;

namespace Yamaanco.Application.Features.Notifications.Handlers.Queries
{
    public class GetUnSeenGeneralNotificationCountHanlder : IRequestHandler<GetUnSeenGeneralNotificationCountQuery, Response<int>>
    {
        private readonly INotificationsRepository _saredNotificationsCollection;
        private readonly IAccountService _accountService;

        public GetUnSeenGeneralNotificationCountHanlder(INotificationsRepository saredNotificationsCollection, IAccountService accountService)
        {
            _accountService = accountService;
            _saredNotificationsCollection = saredNotificationsCollection;
        }

        public async Task<Response<int>> Handle(GetUnSeenGeneralNotificationCountQuery request, CancellationToken cancellationToken)
        {
            var currentUser = _accountService.GetCurrentUser();
            var numberOfProfileNotification = await _saredNotificationsCollection
                    .GetNumberOfUnSeenGeneralNotification(currentUser.Id);

            return new Response<int>(numberOfProfileNotification, "");
        }
    }
}