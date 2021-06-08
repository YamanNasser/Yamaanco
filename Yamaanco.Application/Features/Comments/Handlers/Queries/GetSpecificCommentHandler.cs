using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Yamaanco.Application.ApiResponses;
using Yamaanco.Application.DTOs.Comment;
using Yamaanco.Application.Features.Comments.Queries;
using Yamaanco.Application.Interfaces;
using Yamaanco.Application.Interfaces.Repositories.Comments;

namespace Yamaanco.Application.Features.Comments.Handlers.Queries
{
    public class GetSpecificCommentHandler : IRequestHandler<GetSpecificCommentQuery, Response<IEnumerable<CommentDto>>>
    {
        private readonly ICommentsRepository _commentsRepository;
        private readonly IMediator _mediator;
        private readonly IAccountService _accountService;

        public GetSpecificCommentHandler(IMediator mediator, IAccountService accountService, ICommentsRepository commentsRepository)
        {
            _commentsRepository = commentsRepository;
            _mediator = mediator;
            _accountService = accountService;
        }

        public async Task<Response<IEnumerable<CommentDto>>> Handle(GetSpecificCommentQuery request, CancellationToken cancellationToken)
        {
            var currentUser = _accountService.GetCurrentUser();

            var response = await _commentsRepository.GetCommentIncludeReplies(currentUser.Id, request.CommentId);

            return new Response<IEnumerable<CommentDto>>(response, $"Comment successfully retrieved.");
        }
    }
}