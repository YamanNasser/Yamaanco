using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Yamaanco.Application.Common.Responses;
using Yamaanco.Application.DTOs.Profile;
using Yamaanco.Application.Features.GroupFollowers.Queries;
using Yamaanco.Application.Interfaces;

namespace Yamaanco.Application.Features.GroupFollowers.Handlers.Queries
{
    public class GetProfileFollowersListHandlers : IRequestHandler<GetProfileFollowersQuery, PagedResponse<IEnumerable<ProfileFollowersNotificationListInfoDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IElapsedTime _elapsedTime;
        private readonly IAccountService _accountService;
        public GetProfileFollowersListHandlers(IUnitOfWork unitOfWork, IAccountService accountService, IElapsedTime elapsedTime)
        {
            _unitOfWork = unitOfWork;
            _elapsedTime = elapsedTime;
            _accountService = accountService;
        }

        public async Task<PagedResponse<IEnumerable<ProfileFollowersNotificationListInfoDto>>> Handle(GetProfileFollowersQuery request, CancellationToken cancellationToken)
        {
            var currentUser = _accountService.GetCurrentUser();
            var profileFollowers = await _unitOfWork.ProfileFollowerRepository
                .GetProfileFollowers(currentUser.Id, request.PageIndex, request.PageSize, request.IsSeen);

            var profileFollowersList = profileFollowers
                .Select(o => new ProfileFollowersNotificationListInfoDto()
                {
                    FollowerId = o.FollowerId,
                    UserName = o.UserName,
                    Gender = o.Gender,
                    ProfilePhotoResources = o.ProfilePhotoResources,
                    ProfileId = o.ProfileId,
                    IsProfileOwnerFollowedTheFollower = o.IsProfileOwnerFollowedTheFollower,
                    ElapsedTime = _elapsedTime.Calculate(o.FollowedDate),
                    IsSeen = o.IsSeen
                })?.ToList();

            return new PagedResponse<IEnumerable<ProfileFollowersNotificationListInfoDto>>(profileFollowersList, request.PageIndex, request.PageSize, profileFollowersList.Count);
        }
    }
}