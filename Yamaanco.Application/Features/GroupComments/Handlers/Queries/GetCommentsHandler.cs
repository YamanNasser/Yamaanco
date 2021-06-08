using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Yamaanco.Application.Common.Responses;
using Yamaanco.Application.DTOs.Comment;
using Yamaanco.Application.Features.GroupComments.Notifications;
using Yamaanco.Application.Features.GroupComments.Queries;
using Yamaanco.Application.Interfaces;

namespace Yamaanco.Application.Features.GroupComments.Handlers.Queries
{
    public class GetCommentsHandler : IRequestHandler<GetCommentsQuery, PagedResponse<IEnumerable<CommentDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMediator _mediator;
        private readonly IAccountService _accountService;

        public GetCommentsHandler(IUnitOfWork unitOfWork, IMediator mediator, IAccountService accountService)
        {
            _unitOfWork = unitOfWork;
            _mediator = mediator;
            _accountService = accountService;
        }

        public async Task<PagedResponse<IEnumerable<CommentDto>>> Handle(GetCommentsQuery request, CancellationToken cancellationToken)
        {
            var currentUser = _accountService.GetCurrentUser();
            var response = await _unitOfWork
                .GroupCommentRepository
                .GetComments(request.GroupId, currentUser.Id, request.PageIndex, request.PageSize);

            //await _mediator.Publish(
            //      new CommentsReceived
            //      {
            //          ReceivedResult = response,
            //          ViewerId = currentUser.Id
            //      },
            //  cancellationToken);

            return new PagedResponse<IEnumerable<CommentDto>>(response, request.PageIndex, request.PageSize, response.Count);
        }
    }
}