using MediatR;
using System.Collections.Generic;
using Yamaanco.Application.Common.Responses;
using Yamaanco.Application.DTOs.Group;

namespace Yamaanco.Application.Features.Groups.Queries
{
    public class GetUserGroupsQuery : IRequest<PagedResponse<IEnumerable<GroupDto>>>
    {
        public string UserId { get; set; }
        public int PageSize { get; set; } = 5;
        public int PageIndex { get; set; } = 0;
    }
}