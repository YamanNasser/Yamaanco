using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Yamaanco.Application.ApiResponses;
using Yamaanco.Application.Common.Exceptions;
using Yamaanco.Application.DTOs.Group;
using Yamaanco.Application.Features.Groups.Queries;
using Yamaanco.Application.Interfaces;
using Yamaanco.Domain.Entities.GroupEntities;

namespace Yamaanco.Application.Features.Groups.Handlers.Queries
{
    public class GetGroupByIdHandler : IRequestHandler<GetGroupByIdQuery, Response<GroupDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IAccountService _accountService;

        public GetGroupByIdHandler(IUnitOfWork unitOfWork, IMapper mapper, IAccountService accountService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _accountService = accountService;
        }

        public async Task<Response<GroupDto>> Handle(GetGroupByIdQuery request, CancellationToken cancellationToken)
        {
            var currentUser = _accountService.GetCurrentUser();
            var group = await _unitOfWork.GroupRepository.GetGroupById(request.Id, currentUser.Id);

            if ((group == null)
            || (!group.IsUserMemberOfGroup && group.GroupTypeId == GroupType.Hidden))//Hidden Group Case.
            {
                throw new NotFoundException(nameof(GroupDto), request.Id);
            }

            return new Response<GroupDto>(group, $"Group Created");
        }
    }
}