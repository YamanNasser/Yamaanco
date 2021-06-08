using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Yamaanco.Application.ApiResponses;
using Yamaanco.Application.Common.Responses;
using Yamaanco.Application.DTOs.Profile;
using Yamaanco.Application.Features.ProfileViewers.Queries;
using Yamaanco.Application.Interfaces;

namespace Yamaanco.Application.Features.ProfileViewers.Handlers.Queries
{
    public class GetProfileViewersCountHandlersQuery : IRequestHandler<GetProfileViewersCountQuery, Response<int>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetProfileViewersCountHandlersQuery(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Response<int>> Handle(GetProfileViewersCountQuery request, CancellationToken cancellationToken)
        {
            var count = await _unitOfWork.ProfileViewerRepository
               .CountAsync(o => o.ProfileId == request.ProfileId);

            return new Response<int>(Convert.ToInt32(count), "");
        }
    }
}