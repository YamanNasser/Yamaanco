using MediatR;
using System.Collections.Generic;
using Yamaanco.Application.Common.Responses;
using Yamaanco.Application.DTOs.Profile;

namespace Yamaanco.Application.Features.GroupFollowers.Queries
{
    public class GetProfileFollowersQuery :
        IRequest<PagedResponse<IEnumerable<ProfileFollowersNotificationListInfoDto>>>
    {
        public bool? IsSeen { get; set; }
        public int PageSize { get; set; } = 5;
        public int PageIndex { get; set; } = 0;
    }
}