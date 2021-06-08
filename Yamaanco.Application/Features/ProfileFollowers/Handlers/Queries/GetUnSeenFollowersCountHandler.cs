using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using Yamaanco.Application.ApiResponses;
using Yamaanco.Application.Features.GroupFollowers.Queries;
using Yamaanco.Application.Interfaces;

namespace Yamaanco.Application.Features.GroupFollowers.Handlers.Queries
{
    public class GetUnSeenFollowersCountHandler : IRequestHandler<GetUnSeenFollowersCountQuery, Response<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAccountService _accountService;
        public GetUnSeenFollowersCountHandler(IUnitOfWork unitOfWork, IAccountService accountService)
        {
            _unitOfWork = unitOfWork;
            _accountService = accountService;
        }

        public async Task<Response<int>> Handle(GetUnSeenFollowersCountQuery request, CancellationToken cancellationToken)
        {
            var currentUser = _accountService.GetCurrentUser();
            var count = await _unitOfWork.ProfileFollowerRepository
                .CountAsync(o => o.ProfileId == currentUser.Id && !o.IsSeen);

            return new Response<int>(Convert.ToInt32(count), "");
        }
    }
}