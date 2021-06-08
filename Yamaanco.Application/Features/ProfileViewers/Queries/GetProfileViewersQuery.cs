using MediatR;
using System.Collections.Generic;
using Yamaanco.Application.Common.Responses;
using Yamaanco.Application.DTOs.Profile;

namespace Yamaanco.Application.Features.ProfileViewers.Queries
{
    public class GetProfileViewersQuery : IRequest<PagedResponse<IEnumerable<ProfileViewersBasicListInfoDto>>>
    {
        public int PageSize { get; set; } = 5;
        public int PageIndex { get; set; } = 0;
    }
}