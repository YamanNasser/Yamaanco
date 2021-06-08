using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Yamaanco.Application.ApiResponses;
using Yamaanco.Application.Common.Exceptions;
using Yamaanco.Application.Features.Profiles.Commands;
using Yamaanco.Application.Interfaces;
using Yamaanco.Domain.Entities.ProfileEntities;

namespace Yamaanco.Application.Features.Profiles.Handlers.Commands
{
    public class DeleteProfileCommandHandler : IRequestHandler<DeleteProfileCommand, Response<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAccountService _accountService;

        public DeleteProfileCommandHandler(IUnitOfWork unitOfWork, IAccountService accountService)
        {
            _unitOfWork = unitOfWork;
            _accountService = accountService;
        }

        public async Task<Response<string>> Handle(DeleteProfileCommand request, CancellationToken cancellationToken)
        {
            var currentUser = _accountService.GetCurrentUser();

            var profile = await _unitOfWork.ProfileRepository.SingleOrDefaultAsync(o => o.Id == request.Id);

            if (profile.CreatedById != currentUser.Id)
                throw new NotFoundException(nameof(Profile), request.Id);

            profile.Delete();
            await _unitOfWork.CommitAsync();

            return new Response<string>(profile.Id, message: $"Profile deleted successfully");
        }
    }
}