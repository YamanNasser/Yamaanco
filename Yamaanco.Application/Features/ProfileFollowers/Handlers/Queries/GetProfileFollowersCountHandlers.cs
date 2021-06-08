using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using Yamaanco.Application.ApiResponses;
using Yamaanco.Application.Features.ProfileFollowers.Queries;
using Yamaanco.Application.Interfaces;


namespace Yamaanco.Application.Features.ProfileFollowers.Handlers.Queries
{
    public class GetProfileFollowersCountHandlers : IRequestHandler<GetProfileFollowersCountQuery, Response<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAccountService _accountService;

        public GetProfileFollowersCountHandlers(IUnitOfWork unitOfWork, IAccountService accountService)
        {
            _unitOfWork = unitOfWork;
            _accountService = accountService;
        }
         

        public async Task<Response<int>> Handle(GetProfileFollowersCountQuery request, CancellationToken cancellationToken)
        {
            var currentUser = _accountService.GetCurrentUser();
            var count = await _unitOfWork.ProfileFollowerRepository
                .CountAsync(o => o.ProfileId == request.ProfileId);

            return new Response<int>(Convert.ToInt32(count), "");
        }
    }
}