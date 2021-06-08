using MediatR;
using Yamaanco.Application.ApiResponses;

namespace Yamaanco.Application.Features.GroupViewers.Commands
{
    public class ViewGroupCommand : IRequest<Response<int>>
    {
        public ViewGroupCommand(string groupId)
        {
            GroupId = groupId;
        }

        public string GroupId { get; set; }
    }
}