using MediatR;
using System.Collections.Generic;
using Yamaanco.Application.Common.Responses;
using Yamaanco.Application.DTOs.Group;

namespace Yamaanco.Application.Features.GroupViewers.Queries
{
    public class GetGroupViewersQuery : IRequest<PagedResponse<IEnumerable<GroupViewersBasicListInfoDto>>>
    {
        public string GroupId { get; set; }
        public int PageSize { get; set; } = 5;
        public int PageIndex { get; set; } = 0;
    }
}