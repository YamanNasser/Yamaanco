using MediatR;
using Yamaanco.Application.ApiResponses;

namespace Yamaanco.Application.Features.Profiles.Commands
{
    public class UpdateProfileCommand : IRequest<Response<string>>
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string AboutMe { get; set; }
        public int GenderId { get; set; }
        public string BirthDate { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
    }
}