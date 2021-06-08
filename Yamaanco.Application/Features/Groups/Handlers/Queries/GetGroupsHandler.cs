using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Yamaanco.Application.Common.Responses;
using Yamaanco.Application.DTOs.Group;
using Yamaanco.Application.Features.Groups.Queries;
using Yamaanco.Application.Interfaces;

namespace Yamaanco.Application.Features.Groups.Handlers.Queries
{
    public class GetGroupsHandler : IRequestHandler<GetGroupsQuery, PagedResponse<IEnumerable<GroupFilterResultDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetGroupsHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<PagedResponse<IEnumerable<GroupFilterResultDto>>> Handle(GetGroupsQuery request, CancellationToken cancellationToken)
        {
            var response = await _unitOfWork
                .GroupRepository
                .GetAllGroupIncludingResourcesOnly(request.Filter, request.SortColumn, request.SortColumnDirection, request.PageIndex, request.PageSize);

            return new PagedResponse<IEnumerable<GroupFilterResultDto>>(response, request.PageIndex, request.PageSize, response.Count);
        }
    }
}