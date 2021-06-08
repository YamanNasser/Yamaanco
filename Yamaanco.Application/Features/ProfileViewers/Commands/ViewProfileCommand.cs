using MediatR;
using Yamaanco.Application.ApiResponses;

namespace Yamaanco.Application.Features.ProfileViewers.Commands
{
    public class ViewProfileCommand : IRequest<Response<int>>
    {
        public ViewProfileCommand(string profileId)
        {
            ProfileId = profileId;
        }

        public string ProfileId { get; set; }
    }
}