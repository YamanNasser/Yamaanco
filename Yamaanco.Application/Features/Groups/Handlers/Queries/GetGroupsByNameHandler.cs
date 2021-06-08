using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Yamaanco.Application.Common.Responses;
using Yamaanco.Application.DTOs.Group;
using Yamaanco.Application.Features.Groups.Queries;
using Yamaanco.Application.Interfaces;

namespace Yamaanco.Application.Features.Groups.Handlers.Queries
{
    public class GetGrouspByNameHandler : IRequestHandler<GetGroupsByNameQuery, PagedResponse<IEnumerable<GroupDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAccountService _accountService;

        public GetGrouspByNameHandler(IUnitOfWork unitOfWork, IAccountService accountService)
        {
            _unitOfWork = unitOfWork;
            _accountService = accountService;
        }

        public async Task<PagedResponse<IEnumerable<GroupDto>>> Handle(GetGroupsByNameQuery request, CancellationToken cancellationToken)
        {
            var currentUser = _accountService.GetCurrentUser();
            var groupList = await _unitOfWork.GroupRepository.GetGroupByNameAndIncludingResourcesOnly(request.NameSearch, request.PageIndex, request.PageSize, currentUser.Id);

            return new PagedResponse<IEnumerable<GroupDto>>(groupList, request.PageIndex, request.PageSize, groupList.Count);
        }
    }
}