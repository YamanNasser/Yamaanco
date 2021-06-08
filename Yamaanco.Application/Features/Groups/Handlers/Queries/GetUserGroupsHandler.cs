using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Yamaanco.Application.Common.Responses;
using Yamaanco.Application.DTOs.Group;
using Yamaanco.Application.Features.Groups.Queries;
using Yamaanco.Application.Interfaces;
using Yamaanco.Domain.Entities.GroupEntities;

namespace Yamaanco.Application.Features.Groups.Handlers.Queries
{
    public class GetUserGroupsHandler : IRequestHandler<GetUserGroupsQuery, PagedResponse<IEnumerable<GroupDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAccountService _accountService;

        public GetUserGroupsHandler(IUnitOfWork unitOfWork, IAccountService accountService)
        {
            _unitOfWork = unitOfWork;
            _accountService = accountService;
        }

        public async Task<PagedResponse<IEnumerable<GroupDto>>> Handle(GetUserGroupsQuery request, CancellationToken cancellationToken)
        {
            var currentUser = _accountService.GetCurrentUser();

            IList<GroupDto> groupList = new List<GroupDto>();

            if (currentUser.Id != request.UserId)//case requested user not current logged in.
            {
                var isSelectedUserFriendWithCurrentUser = await _unitOfWork.ProfileFriendRepository.AnyAsync(o => o.FriendProfileId == request.UserId && o.ProfileId == currentUser.Id);

                if (isSelectedUserFriendWithCurrentUser) //Case if not friends, then get all public and private group.
                {
                    groupList = await _unitOfWork.GroupRepository.GetGroupUserAndIncludingResourcesOnly(request.UserId, request.PageIndex, request.PageSize, GroupType.Public, GroupType.Private);
                }
                else //Case if not friends, then get all public group.
                {
                    groupList = await _unitOfWork.GroupRepository.GetGroupUserAndIncludingResourcesOnly(request.UserId, request.PageIndex, request.PageSize, GroupType.Public);
                }
            }

            groupList = await _unitOfWork.GroupRepository.GetGroupUserAndIncludingResourcesOnly(request.UserId, request.PageIndex, request.PageSize);

            return new PagedResponse<IEnumerable<GroupDto>>(groupList, request.PageIndex, request.PageSize, groupList.Count);
        }
    }
}