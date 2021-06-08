using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Yamaanco.Application.Common.Responses;
using Yamaanco.Application.DTOs.Comment;
using Yamaanco.Application.Features.Comments.Queries;
using Yamaanco.Application.Interfaces;
using Yamaanco.Application.Interfaces.Repositories.Comments;

namespace Yamaanco.Application.Features.Comments.Handlers.Queries
{
    public class GetCommentsHandler : IRequestHandler<GetCommentsQuery, PagedResponse<IEnumerable<CommentDto>>>
    {
        private readonly ICommentsRepository _commentsRepository;
        private readonly IAccountService _accountService;

        public GetCommentsHandler(IAccountService accountService, ICommentsRepository commentsRepository)
        {
            _commentsRepository = commentsRepository;
            _accountService = accountService;
        }

        public async Task<PagedResponse<IEnumerable<CommentDto>>> Handle(GetCommentsQuery request, CancellationToken cancellationToken)
        {
            var currentUser = _accountService.GetCurrentUser();

            var response = await _commentsRepository.GetComments(currentUser.Id, request.PageIndex, request.PageSize);

            return new PagedResponse<IEnumerable<CommentDto>>(response, request.PageIndex, request.PageSize, response.Count);
        }
    }
}