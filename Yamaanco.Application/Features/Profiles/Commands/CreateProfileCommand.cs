using MediatR;
using Yamaanco.Application.ApiResponses;

namespace Yamaanco.Application.Features.Profiles.Commands
{
    public class CreateProfileCommand : IRequest<Response<string>>
    {
        public string AboutMe { get; set; }
        public int GenderId { get; set; }
        public string BirthDate { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
    }
}