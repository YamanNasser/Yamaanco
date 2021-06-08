using MediatR;
using Yamaanco.Application.ApiResponses;
using Yamaanco.Application.Features.Profiles.ViewModel;

namespace Yamaanco.Application.Features.Profiles.Queries
{
    public class GetProfileByIdQuery : IRequest<Response<ProfileView>>
    {
        public string Id { get; set; }
    }
}