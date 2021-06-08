using MediatR;
using Yamaanco.Application.ApiResponses;
using Yamaanco.Application.DTOs.Group;

namespace Yamaanco.Application.Features.Groups.Queries
{
    public class GetGroupByIdQuery : IRequest<Response<GroupDto>>
    {
        public string Id { get; set; }
    }
}