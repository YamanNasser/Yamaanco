using AutoMapper;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Yamaanco.Application.ApiResponses;
using Yamaanco.Application.Common.Exceptions;
using Yamaanco.Application.DTOs.Profile;
using Yamaanco.Application.Features.Profiles.Queries;
using Yamaanco.Application.Features.Profiles.ViewModel;
using Yamaanco.Application.Interfaces;

namespace Yamaanco.Application.Features.Profiles.Handlers.Queries
{
    public class GetProfileByIdHandler : IRequestHandler<GetProfileByIdQuery, Response<ProfileView>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IAccountService _accountService;

        public GetProfileByIdHandler(IUnitOfWork unitOfWork, IAccountService accountService, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _accountService = accountService;
            _mapper = mapper;
        }

        public async Task<Response<ProfileView>> Handle(GetProfileByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await _unitOfWork.ProfileRepository.GetProfileById(request.Id);

            if (entity == null)
            {
                throw new NotFoundException(nameof(ProfileDto), request.Id);
            }

            var profileDto = _mapper.Map<ProfileDto>(entity);
            var currentUser = _accountService.GetCurrentUser();
            return new Response<ProfileView>(new ProfileView()
            {
                Profile = profileDto,
                IsFollowedByLoggedInUser =
                entity.FollowersId
                .Any(o => o == currentUser.Id)
            }, "");
        }
    }
}