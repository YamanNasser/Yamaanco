using MediatR;
using Yamaanco.Application.ApiResponses;

namespace Yamaanco.Application.Features.GroupMembers.Commands
{
    public class SendEmailRequestToNewMemberRequest
    {
        public string GroupId { get; set; }
        public string InvitedEmail { get; set; }
    }

    public class SendRequestToNewMemberCommand : IRequest<Response<bool>>
    {
        public SendRequestToNewMemberCommand(string groupId, string origin, string invitedEmail)
        {
            GroupId = groupId;
            Origin = origin;
            InvitedEmail = invitedEmail;
        }

        public string GroupId { get; set; }
        public string Origin { get; set; }
        public string InvitedEmail { get; set; }
    }
}