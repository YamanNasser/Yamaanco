using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Yamaanco.Application.ApiResponses;
using Yamaanco.Application.Features.GroupFollowers.Commands;
using Yamaanco.Application.Interfaces;

namespace Yamaanco.Application.Features.GroupFollowers.Handlers.Commands
{
    internal class UnFollowProfileHandler : IRequestHandler<UnFollowProfileCommand, Response<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAccountService _accountService;

        public UnFollowProfileHandler(IUnitOfWork unitOfWork, IAccountService accountService)
        {
            _unitOfWork = unitOfWork;
            _accountService = accountService;
        }

        public async Task<Response<int>> Handle(UnFollowProfileCommand request, CancellationToken cancellationToken)
        {
            var currentUser = _accountService.GetCurrentUser();

            var num = await _unitOfWork.ProfileFollowerRepository.DeleteProfileFollower(request.ProfileId, currentUser.Id);

            return new Response<int>(num, $"Number of current profile followers are:{num}");
        }
    }
}