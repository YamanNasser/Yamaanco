using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Yamaanco.Application.ApiResponses;
using Yamaanco.Application.Features.ProfileViewers.Commands;
using Yamaanco.Application.Interfaces;

namespace Yamaanco.Application.Features.ProfileViewers.Handlers.Commands
{
    public class ViewProfileHandler : IRequestHandler<ViewProfileCommand, Response<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAccountService _accountService;

        public ViewProfileHandler(IUnitOfWork unitOfWork, IAccountService accountService)
        {
            _unitOfWork = unitOfWork;
            _accountService = accountService;
        }

        public async Task<Response<int>> Handle(ViewProfileCommand request, CancellationToken cancellationToken)
        {
            var currentUser = _accountService.GetCurrentUser();

            var numberOfViewers = await _unitOfWork.ProfileViewerRepository.CreateProfileViewer(request.ProfileId, currentUser.Id);

            return new Response<int>(numberOfViewers, $"Number of  profile viewers are {numberOfViewers}");
        }
    }
}