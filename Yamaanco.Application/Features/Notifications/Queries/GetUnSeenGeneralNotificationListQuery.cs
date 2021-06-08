using MediatR;
using System.Collections.Generic;
using Yamaanco.Application.Common.Responses;
using Yamaanco.Application.Features.Notifications.ViewModel;

namespace Yamaanco.Application.Features.Notifications.Queries
{
    public class GetUnSeenGeneralNotificationListQuery : IRequest<PagedResponse<IEnumerable<UnSeenNotificationsListView>>>
    {
        public int PageSize { get; set; } = 10;
        public int PageIndex { get; set; } = 0;
    }
}