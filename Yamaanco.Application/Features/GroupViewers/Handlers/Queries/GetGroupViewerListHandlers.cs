using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Yamaanco.Application.Common.Responses;
using Yamaanco.Application.DTOs.Group;
using Yamaanco.Application.Features.GroupViewers.Queries;
using Yamaanco.Application.Interfaces;

namespace Yamaanco.Application.Features.GroupViewers.Handlers.Queries
{
    public class GetGroupViewerListHandlers : IRequestHandler<GetGroupViewersQuery, PagedResponse<IEnumerable<GroupViewersBasicListInfoDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IElapsedTime _elapsedTime;

        public GetGroupViewerListHandlers(IUnitOfWork unitOfWork, IElapsedTime elapsedTime)
        {
            _unitOfWork = unitOfWork;
            _elapsedTime = elapsedTime;
        }

        public async Task<PagedResponse<IEnumerable<GroupViewersBasicListInfoDto>>> Handle(GetGroupViewersQuery request, CancellationToken cancellationToken)
        {
            var viewers = await _unitOfWork
                .GroupViewerRepository
                .GetGroupViewers(request.GroupId, request.PageIndex, request.PageSize);

            if (viewers == null)
            {
                return null;
            }

            var profileViewersList = viewers
                .Select(o => new GroupViewersBasicListInfoDto()
                {
                    ViewerId = o.ViewerId,
                    UserName = o.UserName,
                    Gender = o.Gender,
                    ViewerPhotoResources = o.ProfilePhotoResources,
                    GroupId = o.GroupId,
                    ElapsedTime = _elapsedTime.Calculate(o.ViewedDate)
                }).ToList();

            return new PagedResponse<IEnumerable<GroupViewersBasicListInfoDto>>(profileViewersList, request.PageIndex, request.PageSize, profileViewersList.Count);
        }
    }
}