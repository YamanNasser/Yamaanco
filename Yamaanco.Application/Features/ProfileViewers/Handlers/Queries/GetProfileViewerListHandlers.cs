using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Yamaanco.Application.Common.Responses;
using Yamaanco.Application.DTOs.Profile;
using Yamaanco.Application.Features.ProfileViewers.Queries;
using Yamaanco.Application.Interfaces;

namespace Yamaanco.Application.Features.ProfileViewers.Handlers.Queries
{
    public class GetProfileViewerListHandlers : IRequestHandler<GetProfileViewersQuery, PagedResponse<IEnumerable<ProfileViewersBasicListInfoDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IElapsedTime _elapsedTime;
        private readonly IAccountService _accountService;
        public GetProfileViewerListHandlers(IUnitOfWork unitOfWork, IAccountService accountService, IElapsedTime elapsedTime)
        {
            _unitOfWork = unitOfWork;
            _elapsedTime = elapsedTime;
            _accountService = accountService;
        }



        public async Task<PagedResponse<IEnumerable<ProfileViewersBasicListInfoDto>>> Handle(GetProfileViewersQuery request, CancellationToken cancellationToken)
        {
            var currentUser = _accountService.GetCurrentUser();
            var viewers = await _unitOfWork
                .ProfileViewerRepository
                .GetProfileViewers(currentUser.Id, request.PageIndex, request.PageSize);

            if (viewers == null)
            {
                return null;
            }

            var profileViewersList = viewers
                .Select(o => new ProfileViewersBasicListInfoDto()
                {
                    ViewerId = o.ViewerId,
                    UserName = o.UserName,
                    Gender = o.Gender,
                    ProfilePhotoResources = o.ProfilePhotoResources,
                    ProfileId = o.ProfileId,
                    ElapsedTime = _elapsedTime.Calculate(o.ViewedDate)
                }).ToList();

            return new PagedResponse<IEnumerable<ProfileViewersBasicListInfoDto>>(profileViewersList, request.PageIndex, request.PageSize, profileViewersList.Count);
        }
    }
}