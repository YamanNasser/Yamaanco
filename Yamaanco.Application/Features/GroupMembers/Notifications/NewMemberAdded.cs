using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yamaanco.Application.Features.GroupMembers.Notifications
{
    public class NewMemberAdded : INotification
    {
        public string MemberId { get; set; }
        public string AddedById { get; set; }
        public string GroupId { get; set; }
        public string GroupName { get; set; }
        public string AddedByName { get; set; }
        public string Email { get; set; }
        public string URL { get; set; }
    }
}