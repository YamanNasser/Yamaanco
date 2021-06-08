using MediatR;
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
    public class AcceptJoinGroupHandler : IRequestHandler<AcceptJoinGroupCommand, Response<CreatedGroupMemberBasicInfoView>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAccountService _accountService;

        public AcceptJoinGroupHandler(IUnitOfWork unitOfWork, IAccountService accountService)
        {
            _unitOfWork = unitOfWork;
            _accountService = accountService;
        }

        public async Task<Response<CreatedGroupMemberBasicInfoView>> Handle(AcceptJoinGroupCommand request, CancellationToken cancellationToken)
        {
            var currentUser = _accountService.GetCurrentUser();

            var memberRequestInfo = await _unitOfWork.GroupMemberRequestRepository.SingleOrDefaultAsync(o => o.Code == request.Code);

            if (memberRequestInfo == null)
            {
                throw new NotFoundException(nameof(GroupMemberRequest), request.Code);
            }

            if (memberRequestInfo.InvitedEmail != currentUser.Email)
            {
                throw new NotFoundException(nameof(GroupMemberRequest), request.Code);
            }

            //if ((_dateTime.Now - memberRequestInfo.RequestDate).Days > 15)//Time Expired
            //{
            //    throw new NotFoundException(nameof(GroupMemberRequest), request.Code);
            //}

            var groupMember = await _unitOfWork.GroupMemberRepository.CreateGroupMember(memberRequestInfo.GroupId, currentUser.Id);

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

            return new Response<CreatedGroupMemberBasicInfoView>(createdGroupMemberBasicInfoView, $"Successfully joined {groupMember.GroupName} group.");
        }
    }
}