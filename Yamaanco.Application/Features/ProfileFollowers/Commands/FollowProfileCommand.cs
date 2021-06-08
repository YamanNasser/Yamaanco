using MediatR;
using Yamaanco.Application.ApiResponses;

namespace Yamaanco.Application.Features.GroupFollowers.Commands
{
    public class FollowProfileCommand : IRequest<Response<int>>
    {
        public string ProfileId { get; set; }
    }
}