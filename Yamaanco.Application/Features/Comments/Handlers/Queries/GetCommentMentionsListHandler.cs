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
    public class GetCommentMentionsListHandler : IRequestHandler<GetCommentMentionsListQuery, PagedResponse<IEnumerable<CommentMentionsInfoDto>>>
    {
        private readonly ICommentsRepository _commentsRepository;
        private readonly IAccountService _accountService;

        public GetCommentMentionsListHandler(IAccountService accountService, ICommentsRepository commentsRepository)
        {
            _commentsRepository = commentsRepository;
            _accountService = accountService;
        }

        public async Task<PagedResponse<IEnumerable<CommentMentionsInfoDto>>> Handle(GetCommentMentionsListQuery request, CancellationToken cancellationToken)
        {
            var currentUser = _accountService.GetCurrentUser();

            var response = await _commentsRepository.GetMentionsList(currentUser.Id, request.PageIndex, request.PageSize, request.UserName,request.CommentId);

            return new PagedResponse<IEnumerable<CommentMentionsInfoDto>>(response, request.PageIndex, request.PageSize, response.Count);
        }
    }
}