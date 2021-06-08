using MediatR;
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Yamaanco.Application.ApiResponses;
using Yamaanco.Application.Common.Exceptions;
using Yamaanco.Application.Extensions;
using Yamaanco.Application.Features.GroupMembers.Commands;
using Yamaanco.Application.Features.GroupMembers.Notifications;
using Yamaanco.Application.Interfaces;
using Yamaanco.Domain.Entities.GroupEntities;

namespace Yamaanco.Application.Features.GroupMembers.Handlers.Commands
{
    public class SendRequestToNewMemberHandler : IRequestHandler<SendRequestToNewMemberCommand, Response<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMediator _mediator;
        private readonly IDateTime _dateTime;
        private readonly IAccountService _accountService;

        public SendRequestToNewMemberHandler(IUnitOfWork unitOfWork, IMediator mediator, IDateTime dateTime, IAccountService accountService)
        {
            _unitOfWork = unitOfWork;
            _mediator = mediator;
            _dateTime = dateTime;
            _accountService = accountService;
        }

        public async Task<Response<bool>> Handle(SendRequestToNewMemberCommand request, CancellationToken cancellationToken)
        {
            var currentUser = _accountService.GetCurrentUser();

            var isCurrentUserMemberOfRequestedGroup = await _unitOfWork.GroupMemberRepository
                .AnyAsync(o => o.MemberId == currentUser.Id && o.GroupId == request.GroupId);

            var memberId = await _unitOfWork.ProfileRepository.GetProfileIdByEmail(request.InvitedEmail);

            if (!isCurrentUserMemberOfRequestedGroup)
            {
                throw new NotFoundException(nameof(Group), request.GroupId);
            }

            var isRequestedEmailMemberOfGroup = await _unitOfWork.GroupMemberRepository
           .AnyAsync(o => o.MemberId == memberId && o.GroupId == request.GroupId);
            if (isRequestedEmailMemberOfGroup)
            {
                //$"{request.InvitedEmail} is member of {request.GroupId}."
                throw new RequestedUserIsMemberOfGroupException(request.InvitedEmail, request.GroupId);
            }

            var groupName = _unitOfWork.GroupRepository.Get(request.GroupId)?.Name;
            if (groupName == null)
            {
                throw new NotFoundException(nameof(Group), request.GroupId);
            }

            var id = Guid.NewGuid().ToString();
            var date = _dateTime.Now;
            var newRequest = new GroupMemberRequest(
                groupId: request.GroupId,
                invitedEmail: request.InvitedEmail,
                inviterId: currentUser.Id);

            _unitOfWork.GroupMemberRequestRepository.Add(newRequest);

            await _unitOfWork.CommitAsync();
            var code = newRequest.Code;
            var url = GenerateInvitationUrl(code, request.Origin);

            await _mediator.Publish(
                  new GroupMemberEmailRequestCreated
                  {
                      URL = url,
                      GroupId = newRequest.GroupId,
                      InvitedEmail = newRequest.InvitedEmail,
                      InviterId = newRequest.InviterId,
                      InviterName = currentUser.UserName,
                      GroupName = groupName
                  },
              cancellationToken);

            return new Response<bool>(true, message: $"Email has been sent successfully.");
        }

        private string GenerateInvitationUrl(string code, string origin)
        {
            var route = $"api/groupMembers/accept-join/{code}";

            var uri = new Uri(string.Concat($"{origin}/", route)).ToString();

            return uri;
        }
    }
}