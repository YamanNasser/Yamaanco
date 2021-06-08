using MediatR;
using Yamaanco.Application.ApiResponses;

namespace Yamaanco.Application.Features.Groups.Commands
{
    public class DeleteGroupCommand : IRequest<Response<string>>
    {
        public string Id { get; set; }
    }
}