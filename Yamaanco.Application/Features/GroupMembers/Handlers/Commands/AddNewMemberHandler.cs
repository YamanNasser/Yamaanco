using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Yamaanco.Application.ApiResponses;
using Yamaanco.Application.Common.Exceptions;
using Yamaanco.Application.DTOs.Group;
using Yamaanco.Application.Features.GroupMembers.Commands;
using Yamaanco.Application.Features.GroupMembers.Notifications;
using Yamaanco.Application.Features.GroupMembers.ViewModel;
using Yamaanco.Application.Interfaces;
using Yamaanco.Domain.Entities.GroupEntities;

namespace Yamaanco.Application.Features.GroupMembers.Handlers.Commands
{
    public class AddNewMemberHandler : IRequestHandler<AddNewMemberCommand, Response<CreatedGroupMemberBasicInfoView>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMediator _mediator;
        private readonly IDateTime _dateTime;
        private readonly IAccountService _accountService;

        public AddNewMemberHandler(IUnitOfWork unitOfWork, IMediator mediator, IDateTime dateTime, IAccountService accountService)
        {
            _unitOfWork = unitOfWork;
            _mediator = mediator;
            _dateTime = dateTime;
            _accountService = accountService;
        }

        public async Task<Response<CreatedGroupMemberBasicInfoView>> Handle(AddNewMemberCommand request, CancellationToken cancellationToken)
        {
            var currentUser = _accountService.GetCurrentUser();

            var isCurrentUserMemberOfRequestedGroup = await _unitOfWork.GroupMemberRepository.AnyAsync(o => o.MemberId == currentUser.Id && o.GroupId == request.GroupId);

            if (!isCurrentUserMemberOfRequestedGroup)
            {
                throw new AccessDeniedException(nameof(Group), request.GroupId);
            }

            var isRequestedEmailMemberOfGroup = await _unitOfWork.GroupMemberRepository
          .AnyAsync(o => o.MemberId == request.MemberId && o.GroupId == request.GroupId);
            if (isRequestedEmailMemberOfGroup)
            {
                throw new RequestedUserIsMemberOfGroupException(request.MemberId, request.GroupId);
            }

            var groupMember = await _unitOfWork.GroupMemberRepository.CreateGroupMember(request.GroupId, request.MemberId);

            await _mediator.Publish(
                 new NewMemberAdded
                 {
                     AddedById = currentUser.Id,
                     AddedByName = currentUser.UserName,
                     MemberId = groupMember.MemberId,
                     Email = groupMember.Email,
                     GroupId = groupMember.GroupId,
                     GroupName = groupMember.GroupName
                 },
             cancellationToken);

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

            return new Response<CreatedGroupMemberBasicInfoView>(createdGroupMemberBasicInfoView, $"{groupMember.MemberName} is now member of {groupMember.GroupName} group.");
        }
    }
}