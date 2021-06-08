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
    public class FindCommentsHandler : IRequestHandler<FindCommentsQuery, PagedResponse<IEnumerable<CommentDto>>>
    {
        private readonly ICommentsRepository _commentsRepository;
        private readonly IAccountService _accountService;

        public FindCommentsHandler(IAccountService accountService, ICommentsRepository commentsRepository)
        {
            _commentsRepository = commentsRepository;
            _accountService = accountService;
        }

        public async Task<PagedResponse<IEnumerable<CommentDto>>> Handle(FindCommentsQuery request, CancellationToken cancellationToken)
        {
            var currentUser = _accountService.GetCurrentUser();

            var response = await _commentsRepository.FindComments(currentUser.Id, request.PageIndex, request.PageSize, request.Contains, request.FromDate, request.ToDate, request.CreatorName, request.CategoryName);

            return new PagedResponse<IEnumerable<CommentDto>>(response, request.PageIndex, request.PageSize, response.Count);
        }
    }
}