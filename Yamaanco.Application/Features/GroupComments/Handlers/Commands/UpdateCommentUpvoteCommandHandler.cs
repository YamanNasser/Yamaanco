using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Yamaanco.Application.ApiResponses;
using Yamaanco.Application.DTOs.Comment;
using Yamaanco.Application.Features.GroupComments.Commands;
using Yamaanco.Application.Features.GroupComments.Notifications;
using Yamaanco.Application.Interfaces;

namespace Yamaanco.Application.Features.GroupComments.Handlers.Commands
{
    public class UpdateCommentUpvoteCommandHandler : IRequestHandler<UpdateCommentUpvoteCommand, Response<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMediator _mediator;
        private readonly IAccountService _accountService;

        public UpdateCommentUpvoteCommandHandler(IUnitOfWork unitOfWork, IMediator mediator, IAccountService accountService)
        {
            _unitOfWork = unitOfWork;
            _mediator = mediator;
            _accountService = accountService;
        }

        public async Task<Response<int>> Handle(UpdateCommentUpvoteCommand request, CancellationToken cancellationToken)
        {
            var currentUser = _accountService.GetCurrentUser();
            var commentUpvoted = await _unitOfWork.GroupCommentUpvotedUserRepository
                .UpdateCommentUpvoteCommand(request.CommentId, currentUser.Id);

            await CommentUpvoted(commentUpvoted, cancellationToken);

            return new Response<int>(commentUpvoted.UpvoteCount, "Comment voted successfully.");
        }

        private async Task CommentUpvoted(CommentUpvotedDto entity, CancellationToken cancellationToken)
        {
            await _mediator.Publish(
                    new CommentUpvoted
                    {
                        Id = entity.Id,
                        Parent = entity.Parent,
                        Root = entity.Root,
                        Content = entity.Content,
                        UpvoteById = entity.UpvoteById,
                        GroupId = entity.CategoryId,
                        GroupName = entity.CategoryName,
                        UpvoteByName = entity.UpvoteByName,
                        CreatedById = entity.CreatedById
                    },
                cancellationToken);
        }
    }
}