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
    public class GetAllGeneralNotificationListHandler : IRequestHandler<GetAllGeneralNotificationListQuery, PagedResponse<IEnumerable<AllNotificationsListView>>>
    {
        private readonly INotificationsRepository _saredNotificationsCollection;
        private IElapsedTime _elapsedTime;
        private readonly IAccountService _accountService;

        public GetAllGeneralNotificationListHandler(IElapsedTime elapsedTime,
 INotificationsRepository saredNotificationsCollection, IAccountService accountService)
        {
            _elapsedTime = elapsedTime;
            _accountService = accountService;
            _saredNotificationsCollection = saredNotificationsCollection;
        }

        public async Task<PagedResponse<IEnumerable<AllNotificationsListView>>> Handle(GetAllGeneralNotificationListQuery request, CancellationToken cancellationToken)
        {
            var currentUser = _accountService.GetCurrentUser();
            var notificationList = new List<AllNotificationsListView>();

            var response = await _saredNotificationsCollection
                .GetAllGeneralNotifications(currentUser.Id,
                request.PageIndex, request.PageSize, request.IsSeen);

            foreach (var notification in response)
            {
                notificationList.Add(new AllNotificationsListView()
                {
                    CategoryId = notification.CategoryId,
                    CategoryName = notification.CategoryName,
                    Category = notification.CategoryType,
                    CreatedDate = notification.CreatedDate,
                    ElapsedTime = _elapsedTime.Calculate(notification.CreatedDate),
                    GenderId = notification.GenderId,
                    IsSeen = notification.IsSeen,
                    Content = notification.Content,
                    ParticipantPhotoPath = notification.ParticipantPhotoPath,
                    Target = notification.Target
                });
            }
            return new PagedResponse<IEnumerable<AllNotificationsListView>>(notificationList, request.PageIndex, request.PageSize, notificationList.Count);
        }
    }
}