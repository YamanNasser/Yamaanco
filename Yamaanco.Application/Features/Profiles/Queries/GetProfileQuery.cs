using MediatR;
using System.Collections.Generic;
using Yamaanco.Application.Common.Responses;
using Yamaanco.Application.DTOs.Profile;

namespace Yamaanco.Application.Features.Profiles.Queries
{
    public class GetProfileQuery : IRequest<PagedResponse<IEnumerable<ProfileDto>>>
    {
        public string Filter { get; set; }
        public string SortColumn { get; set; }
        public string SortColumnDirection { get; set; }
        public int PageSize { get; set; } = 5;
        public int PageIndex { get; set; } = 0;
    }
}