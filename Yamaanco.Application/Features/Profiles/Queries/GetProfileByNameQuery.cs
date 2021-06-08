using MediatR;
using System.Collections.Generic;
using Yamaanco.Application.Common.Responses;
using Yamaanco.Application.DTOs.Profile;

namespace Yamaanco.Application.Features.Profiles.Queries
{
    public class GetProfileByNameQuery : IRequest<PagedResponse<IEnumerable<ProfileBasicSearchResultDto>>>
    {
        public string NameSearch { get; set; }
        public int PageSize { get; set; } = 5;
        public int PageIndex { get; set; } = 0;
    }
}