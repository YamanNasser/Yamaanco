using MediatR;
using System.Collections.Generic;
using Yamaanco.Application.Common.Responses;
using Yamaanco.Application.DTOs.Group;

namespace Yamaanco.Application.Features.GroupMembers.Queries
{
    public class GetGroupMemberListQuery : IRequest<PagedResponse<IEnumerable<GroupMemberDto>>>
    {
        public string GroupId { get; set; }
        public string MemberNameContains { get; set; }
        public int PageSize { get; set; } = 5;
        public int PageIndex { get; set; } = 0;
    }
}