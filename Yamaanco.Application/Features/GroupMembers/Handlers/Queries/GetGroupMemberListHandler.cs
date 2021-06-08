using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Yamaanco.Application.Common.Exceptions;
using Yamaanco.Application.Common.Responses;
using Yamaanco.Application.DTOs.Group;
using Yamaanco.Application.Features.GroupMembers.Queries;
using Yamaanco.Application.Interfaces;
using Yamaanco.Domain.Entities.GroupEntities;

namespace Yamaanco.Application.Features.GroupMembers.Handlers.Queries
{
    public class GetGroupMemberListHandler : IRequestHandler<GetGroupMemberListQuery, PagedResponse<IEnumerable<GroupMemberDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAccountService _accountService;

        public GetGroupMemberListHandler(IUnitOfWork unitOfWork, IAccountService accountService)
        {
            _unitOfWork = unitOfWork;
            _accountService = accountService;
        }

        public async Task<PagedResponse<IEnumerable<GroupMemberDto>>> Handle(GetGroupMemberListQuery request, CancellationToken cancellationToken)
        {
            var currentUser = _accountService.GetCurrentUser();

            var group = await _unitOfWork.GroupRepository.GetGroupById(request.GroupId, currentUser.Id);

            if (group == null)
            {
                throw new NotFoundException(nameof(GroupDto), request.GroupId);
            }

            //Any user can see the public group members.
            if (!group.IsUserMemberOfGroup && group.GroupTypeId != GroupType.Public)
            {
                throw new AccessDeniedException(nameof(Group), request.GroupId);
            }

            var groupMemberList = await _unitOfWork.GroupMemberRepository
                .GetGroupMembers(request.GroupId, request.MemberNameContains, currentUser.Id, request.PageIndex, request.PageSize);

            return new PagedResponse<IEnumerable<GroupMemberDto>>(groupMemberList, request.PageIndex, request.PageSize, groupMemberList.Count);
        }
    }
}