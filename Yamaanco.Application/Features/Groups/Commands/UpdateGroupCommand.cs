using MediatR;
using Microsoft.AspNetCore.Http;
using Yamaanco.Application.ApiResponses;

namespace Yamaanco.Application.Features.Groups.Commands
{
    public class UpdateGroupCommand : IRequest<Response<string>>
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int GroupTypeId { get; set; }
        public string Description { get; set; }
        public IFormFile Photo { get; set; }
    }
}