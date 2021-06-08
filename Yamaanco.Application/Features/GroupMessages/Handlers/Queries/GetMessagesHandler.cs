using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Yamaanco.Application.Common.Responses;
using Yamaanco.Application.DTOs.Message;
using Yamaanco.Application.Features.GroupMessages.Notifications;
using Yamaanco.Application.Features.GroupMessages.Queries;
using Yamaanco.Application.Interfaces;

namespace Yamaanco.Application.Features.GroupMessages.Handlers.Queries
{
    public class GetMessagesHandler : IRequestHandler<GetMessagesQuery, PagedResponse<IEnumerable<MessageDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMediator _mediator;
        private readonly IAccountService _accountService;

        public GetMessagesHandler(IUnitOfWork unitOfWork, IMediator mediator, IAccountService accountService)
        {
            _unitOfWork = unitOfWork;
            _mediator = mediator;
            _accountService = accountService;
        }

        public async Task<PagedResponse<IEnumerable<MessageDto>>> Handle(GetMessagesQuery request, CancellationToken cancellationToken)
        {
            var currentUser = _accountService.GetCurrentUser();
            var response = await _unitOfWork
                .GroupMessageRepository
                .GetMessagesByTarget(request.Target, request.PageIndex, request.PageSize);

            await _mediator.Publish(
                  new MessagesReceived
                  {
                      ReceivedResult = response,
                      ViewerId = currentUser.Id //Current logged In user is the viewer.
                  },
              cancellationToken);

            return new PagedResponse<IEnumerable<MessageDto>>(response, request.PageIndex, request.PageSize, response.Count);
        }
    }
}