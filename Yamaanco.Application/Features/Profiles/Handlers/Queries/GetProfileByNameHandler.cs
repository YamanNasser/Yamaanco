using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Yamaanco.Application.Common.Responses;
using Yamaanco.Application.DTOs.Profile;
using Yamaanco.Application.Features.Profiles.Queries;
using Yamaanco.Application.Interfaces;

namespace Yamaanco.Application.Features.Profiles.Handlers.Queries
{
    public class GetProfileByNameHandler : IRequestHandler<GetProfileByNameQuery, PagedResponse<IEnumerable<ProfileBasicSearchResultDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAccountService _accountService;

        public GetProfileByNameHandler(IUnitOfWork unitOfWork, IAccountService accountService)
        {
            _unitOfWork = unitOfWork;
            _accountService = accountService;
        }

        public async Task<PagedResponse<IEnumerable<ProfileBasicSearchResultDto>>> Handle(GetProfileByNameQuery request, CancellationToken cancellationToken)
        {
            var currentUser = _accountService.GetCurrentUser();

            var entity = await _unitOfWork.ProfileRepository.GetProfileByName(request.NameSearch, request.PageIndex, request.PageSize);

            var profileBasicInfo = entity
               .Select(o => new ProfileBasicSearchResultDto()
               {
                   Id = o.Id,
                   FirstName = o.FirstName,
                   LastName = o.LastName,
                   UserName = o.UserName,
                   Gender = o.Gender,
                   Email = o.Email,
                   PhoneNumber = o.PhoneNumber,
                   ProfilePhotoResources = o.ProfilePhotoResources,
                   IsFollowedByloggedInUser = o.FollowersId.Any(o => o == currentUser.Id),
                   NumberOfFollowers = o.NumberOfFollowers,
                   NumberOfViewers = o.NumberOfViewers
               })?.ToList();

            return new PagedResponse<IEnumerable<ProfileBasicSearchResultDto>>(profileBasicInfo, request.PageIndex, request.PageSize, profileBasicInfo.Count);
        }
    }
}