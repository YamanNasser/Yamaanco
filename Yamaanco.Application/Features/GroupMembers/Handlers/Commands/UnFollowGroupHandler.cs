using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Yamaanco.Application.ApiResponses;
using Yamaanco.Application.Features.GroupFollowers.Commands;
using Yamaanco.Application.Interfaces;

namespace Yamaanco.Application.Features.GroupMembers.Handlers.Commands
{
    public class UnFollowGroupHandler : IRequestHandler<UnFollowGroupCommand, Response<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAccountService _accountService;

        public UnFollowGroupHandler(IUnitOfWork unitOfWork, IAccountService accountService)
        {
            _unitOfWork = unitOfWork;
            _accountService = accountService;
        }

        public async Task<Response<int>> Handle(UnFollowGroupCommand request, CancellationToken cancellationToken)
        {
            var currentUser = _accountService.GetCurrentUser();

            var num = await _unitOfWork.GroupMemberRepository.DeleteGroupMember(request.GroupId, currentUser.Id);

            return new Response<int>(num, $"Successfully unfollowed group.Current number of group member is {num}");
        }
    }
}