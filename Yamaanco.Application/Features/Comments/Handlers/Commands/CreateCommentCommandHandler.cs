using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Yamaanco.Application.ApiResponses;
using Yamaanco.Application.Common.Exceptions;
using Yamaanco.Application.DTOs.Comment;
using Yamaanco.Application.Features.Comments.Commands;
using Yamaanco.Application.Interfaces;
using Yamaanco.Application.Interfaces.Repositories.Comments;
using Yamaanco.Domain.Enums;
using GroupCommentCommand = Yamaanco.Application.Features.GroupComments.Commands;
using ProfileCommentCommand = Yamaanco.Application.Features.ProfileComments.Commands;

namespace Yamaanco.Application.Features.Comments.Handlers.Commands
{
    public class CreateCommentCommandHandler : IRequestHandler<CreateCommentCommand, Response<CommentDto>>
    {
        private readonly ICommentsRepository _commentsRepository;
        private readonly IMediator _mediator;
        private readonly IAccountService _accountService;

        public CreateCommentCommandHandler(ICommentsRepository commentsRepository, IMediator mediator, IAccountService accountService)
        {
            _commentsRepository = commentsRepository;
            _mediator = mediator;
            _accountService = accountService;
        }

        public async Task<Response<CommentDto>> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
        {
            var currentUser = _accountService.GetCurrentUser();

            if (request.Root == null)
            {
                var command = new ProfileCommentCommand.CreateCommentCommand()
                {
                    Attachments = request.Attachments,
                    Root = request.Root,
                    Parent = request.Parent,
                    Content = request.Content,
                    Pings = request.Pings,
                    ProfileId = request.CategoryId
                };
                return await _mediator.Send(command);
            }
            else
            {
                var category = await _commentsRepository.GetCommentCategory(currentUser.Id, request.Root);

                switch (category)
                {
                    case CommentCategory.Profile:
                        {
                            var command = new ProfileCommentCommand.CreateCommentCommand()
                            {
                                Attachments = request.Attachments,
                                Root = request.Root,
                                Parent = request.Parent,
                                Content = request.Content,
                                Pings = request.Pings,
                                ProfileId = request.CategoryId
                            };
                            return await _mediator.Send(command, cancellationToken);
                        }
                    case CommentCategory.Group:
                        {
                            var createGroupCommentCommand = new GroupCommentCommand.CreateCommentCommand()
                            {
                                Attachments = request.Attachments,
                                Root = request.Root,
                                Parent = request.Parent,
                                Content = request.Content,
                                Pings = request.Pings,
                                GroupId = request.CategoryId
                            };
                            return await _mediator.Send(createGroupCommentCommand, cancellationToken);
                        }
                    default:
                        {
                            throw new NotFoundException("Comment", request.Root);
                        }
                }
            }
        }
    }
}