using MediatR;
using Yamaanco.Application.ApiResponses;

namespace Yamaanco.Application.Features.GroupFollowers.Commands
{
    public class UnFollowGroupCommand : IRequest<Response<int>>
    {
        public string GroupId { get; set; }

        public UnFollowGroupCommand(string groupId)
        {
            GroupId = groupId;
        }
    }
}