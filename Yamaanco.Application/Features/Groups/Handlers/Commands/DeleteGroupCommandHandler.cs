using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Yamaanco.Application.ApiResponses;
using Yamaanco.Application.Common.Exceptions;
using Yamaanco.Application.Features.Groups.Commands;
using Yamaanco.Application.Interfaces;
using Yamaanco.Domain.Entities.GroupEntities;

namespace Yamaanco.Application.Features.Groups.Handlers.Commands
{
    public class DeleteGroupCommandHandler : IRequestHandler<DeleteGroupCommand, Response<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAccountService _accountService;

        public DeleteGroupCommandHandler(IUnitOfWork unitOfWork, IAccountService accountService)
        {
            _unitOfWork = unitOfWork;
            _accountService = accountService;
        }

        public async Task<Response<string>> Handle(DeleteGroupCommand request, CancellationToken cancellationToken)
        {
            //Only Group Creator/Group Admin can delete (i.e. The delete here is not a physical delete it just a flag 0 or 1) the group.
            var currentUser = _accountService.GetCurrentUser();
            var group = await _unitOfWork.GroupRepository.SingleOrDefaultAsync(o => o.Id == request.Id);

            var isCurrentUserIsGroupAdmin = await _unitOfWork.GroupMemberRepository.AnyAsync(o => o.MemberId == currentUser.Id && o.GroupId == request.Id && o.IsAdmin);

            if (!isCurrentUserIsGroupAdmin)
            {
                throw new NotFoundException(nameof(Group), request.Id);
            }

            group.Delete();
            await _unitOfWork.CommitAsync();

            return new Response<string>(group.Id, "Group deleted successfully.");
        }
    }
}