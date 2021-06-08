using MediatR;
using Yamaanco.Application.ApiResponses;
using Yamaanco.Application.DTOs.Group;
using Yamaanco.Application.Features.GroupMembers.ViewModel;

namespace Yamaanco.Application.Features.GroupMembers.Commands
{
    public class FollowGroupCommand : IRequest<Response<CreatedGroupMemberBasicInfoView>>
    {
        public string GroupId { get; set; }

        public FollowGroupCommand(string groupId)
        {
            GroupId = groupId;
        }
    }
}