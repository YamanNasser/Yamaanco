using MediatR;

namespace Yamaanco.Application.Features.GroupMembers.Notifications
{
    public class GroupMemberEmailRequestCreated : INotification
    {
        public string GroupId { get; set; }
        public string GroupName { get; set; }
        public string InviterId { get; set; }
        public string InviterName { get; set; }
        public string InvitedEmail { get; set; }
        public string URL { get; set; }
    }
}