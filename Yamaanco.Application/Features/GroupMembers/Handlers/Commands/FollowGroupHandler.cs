using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Yamaanco.Application.ApiResponses;
using Yamaanco.Application.Common.Exceptions;
using Yamaanco.Application.DTOs.Group;
using Yamaanco.Application.Features.GroupMembers.Commands;
using Yamaanco.Application.Features.GroupMembers.ViewModel;
using Yamaanco.Application.Interfaces;
using Yamaanco.Domain.Entities.GroupEntities;

namespace Yamaanco.Application.Features.GroupMembers.Handlers.Commands
{
    public class FollowGroupHandler : IRequestHandler<FollowGroupCommand, Response<CreatedGroupMemberBasicInfoView>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAccountService _accountService;

        public FollowGroupHandler(IUnitOfWork unitOfWork, IAccountService accountService)
        {
            _unitOfWork = unitOfWork;
            _accountService = accountService;
        }

        public async Task<Response<CreatedGroupMemberBasicInfoView>> Handle(FollowGroupCommand request, CancellationToken cancellationToken)
        {
            var currentUser = _accountService.GetCurrentUser();

            //The user can follow the public and private group directly but the hidden can't.
            var allowGroupType = new[] { GroupType.Private, GroupType.Public };
            var isUserAllowToFollowGroup = await _unitOfWork.GroupRepository.AnyAsync(o => o.Id == request.GroupId && allowGroupType.Contains(o.GroupTypeId));
            if (!isUserAllowToFollowGroup)
            {
                throw new AccessDeniedException(nameof(Group), request.GroupId);
            }

            var groupMember = await _unitOfWork.GroupMemberRepository.CreateGroupMember(request.GroupId, currentUser.Id);

            var createdGroupMemberBasicInfoView = new CreatedGroupMemberBasicInfoView
            {
                GroupId = groupMember.GroupId,
                GroupName = groupMember.GroupName,
                NumberOfGroupMembers = groupMember.NumberOfGroupMembers,
                MemberInfo = new MemberBasicInfoDto()
                {
                    City = groupMember.City,
                    Country = groupMember.Country,
                    Email = groupMember.Email,
                    IsAdmin = groupMember.IsAdmin,
                    JoinDate = groupMember.JoinDate,
                    MemberId = groupMember.MemberId,
                    MemberName = groupMember.MemberName,
                    MemberProfileMediumPhotoPath = groupMember.MemberProfileMediumPhotoPath,
                    PhoneNumber = groupMember.PhoneNumber
                }
            };

            return new Response<CreatedGroupMemberBasicInfoView>(createdGroupMemberBasicInfoView, $"Successfully joined  { groupMember.GroupName} group.");
        }
    }
}