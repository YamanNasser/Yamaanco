using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Yamaanco.Application.ApiResponses;
using Yamaanco.Application.Features.GroupViewers.Commands;
using Yamaanco.Application.Interfaces;

namespace Yamaanco.Application.Features.GroupViewers.Handlers.Commands
{
    public class ViewGroupHandler : IRequestHandler<ViewGroupCommand, Response<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAccountService _accountService;

        public ViewGroupHandler(IUnitOfWork unitOfWork, IAccountService accountService)
        {
            _unitOfWork = unitOfWork;
            _accountService = accountService;
        }

        public async Task<Response<int>> Handle(ViewGroupCommand request, CancellationToken cancellationToken)
        {
            var currentUser = _accountService.GetCurrentUser();

            var numberOfViewers = await _unitOfWork.GroupViewerRepository.CreateGroupViewer(request.GroupId, currentUser.Id);

            return new Response<int>(numberOfViewers, $"Number of  group viewers are {numberOfViewers}");
        }
    }
}