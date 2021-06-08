using MediatR;
using Yamaanco.Application.ApiResponses;

namespace Yamaanco.Application.Features.Profiles.Commands
{
    public class DeleteProfileCommand : IRequest<Response<string>>
    {
        public string Id { get; set; }
    }
}