using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Yamaanco.Application.ApiResponses;
using Yamaanco.Application.Features.GroupFollowers.Commands;
using Yamaanco.Application.Features.GroupFollowers.Notifications.Created;
using Yamaanco.Application.Interfaces;

namespace Yamaanco.Application.Features.GroupFollowers.Handlers.Commands
{
    public class FollowProfileHandler : IRequestHandler<FollowProfileCommand, Response<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDateTime _dateTime;
        private readonly IMediator _mediator;
        private readonly IElapsedTime _elapsedTime;
        private readonly IAccountService _accountService;

        public FollowProfileHandler(IUnitOfWork unitOfWork, IDateTime dateTime, IMediator mediator, IElapsedTime elapsedTime, IAccountService accountService)
        {
            _unitOfWork = unitOfWork;
            _dateTime = dateTime;
            _mediator = mediator;
            _elapsedTime = elapsedTime;
            _accountService = accountService;
        }

        public async Task<Response<int>> Handle(FollowProfileCommand request, CancellationToken cancellationToken)
        {
            var currentUser = _accountService.GetCurrentUser();

            var profileFollowerCreated = await _unitOfWork.ProfileFollowerRepository
                .CreateProfileFollower(request.ProfileId, currentUser.Id);

            if (profileFollowerCreated != null)
            {
                await _mediator.Publish(
                     new FollowerCreated
                     {
                         ProfileId = request.ProfileId,
                         FollowedDate = _dateTime.Now,
                         ProfileEmail = profileFollowerCreated.ProfileEmail,
                         ProfilePhone = profileFollowerCreated.ProfilePhone,
                         ProfileUserName = profileFollowerCreated.ProfileUserName,
                         FollowerId = currentUser.Id,
                         NumberOfUnSeenProfileFollowers = profileFollowerCreated.NumberOfUnSeenProfileFollowers,
                         FollowerName = profileFollowerCreated.FollowerName,
                         FollowerProfileMediumPhotoPath = profileFollowerCreated.FollowerProfileMediumPhotoPath,
                         ElapsedTime = _elapsedTime.Calculate(_dateTime.Now)
                     }, cancellationToken);
            }

            return new Response<int>(profileFollowerCreated.NumberOfProfileFollowers, $"Number of  profile followers are {profileFollowerCreated.NumberOfProfileFollowers}");
        }
    }
}