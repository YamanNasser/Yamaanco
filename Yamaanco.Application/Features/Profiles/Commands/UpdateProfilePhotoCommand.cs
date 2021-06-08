using MediatR;
using Microsoft.AspNetCore.Http;
using Yamaanco.Application.ApiResponses;

namespace Yamaanco.Application.Features.Profiles.Commands
{
    public class UpdateProfilePhotoCommand : IRequest<Response<string>>
    {
        public string Id { get; set; }

        public IFormFile Photo { get; set; }
    }
}