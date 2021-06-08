using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Yamaanco.Application.Common.Responses;
using Yamaanco.Application.Features.Notifications.Queries;
using Yamaanco.Application.Features.Notifications.ViewModel;
using Yamaanco.Application.Interfaces;
using Yamaanco.Application.Interfaces.Repositories.Notifications;

namespace Yamaanco.Application.Features.Notifications.Handlers.Queries
{
    public class GetUnSeenMessageNotificationListHandler : IRequestHandler<GetUnSeenMessageNotificationListQuery, PagedResponse<IEnumerable<UnSeenNotificationsListView>>>
    {
        private readonly INotificationsRepository _saredNotificationsCollection;
        private IElapsedTime _elapsedTime;
        private readonly IAccountService _accountService;

        public GetUnSeenMessageNotificationListHandler(IElapsedTime elapsedTime,
 INotificationsRepository saredNotificationsCollection, IAccountService accountService)
        {
            _elapsedTime = elapsedTime;
            _accountService = accountService;
            _saredNotificationsCollection = saredNotificationsCollection;
        }

        public async Task<PagedResponse<IEnumerable<UnSeenNotificationsListView>>> Handle(GetUnSeenMessageNotificationListQuery request, CancellationToken cancellationToken)
        {
            var currentUser = _accountService.GetCurrentUser();
            var notificationList = new List<UnSeenNotificationsListView>();

            var response = await _saredNotificationsCollection
                .GetUnSeeMessageNotifications(currentUser.Id,
                request.PageIndex, request.PageSize);

            foreach (var notification in response)
            {
                notificationList.Add(new UnSeenNotificationsListView()
                {
                    CategoryId = notification.CategoryId,
                    CategoryName = notification.CategoryName,
                    Category = notification.CategoryType,
                    CreatedDate = notification.CreatedDate,
                    ElapsedTime = _elapsedTime.Calculate(notification.CreatedDate),
                    GenderId = notification.GenderId,
                    Content = notification.Content,
                    ParticipantPhotoPath = notification.ParticipantPhotoPath,
                    Target = notification.Target
                });
            }

            return new PagedResponse<IEnumerable<UnSeenNotificationsListView>>(notificationList, request.PageIndex, request.PageSize, notificationList.Count);
        }
    }
}