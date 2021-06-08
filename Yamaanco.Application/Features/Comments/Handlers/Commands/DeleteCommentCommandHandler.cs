﻿using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Yamaanco.Application.ApiResponses;
using Yamaanco.Application.Common.Exceptions;
using Yamaanco.Application.Features.Comments.Commands;
using Yamaanco.Application.Interfaces;
using Yamaanco.Application.Interfaces.Repositories.Comments;
using Yamaanco.Domain.Enums;
using GroupCommentCommand = Yamaanco.Application.Features.GroupComments.Commands;
using ProfileCommentCommand = Yamaanco.Application.Features.ProfileComments.Commands;

namespace Yamaanco.Application.Features.Comments.Handlers.Commands
{
    public class DeleteCommentCommandHandler : IRequestHandler<DeleteCommentCommand, Response<string>>
    {
        private readonly ICommentsRepository _commentsRepository;
        private readonly IMediator _mediator;
        private readonly IAccountService _accountService;

        public DeleteCommentCommandHandler(ICommentsRepository commentsRepository, IMediator mediator, IAccountService accountService)
        {
            _commentsRepository = commentsRepository;
            _mediator = mediator;
            _accountService = accountService;
        }

        public async Task<Response<string>> Handle(DeleteCommentCommand request, CancellationToken cancellationToken)
        {
            var currentUser = _accountService.GetCurrentUser();

            var category = await _commentsRepository.GetCommentCategory(currentUser.Id, request.CommentId);

            switch (category)
            {
                case CommentCategory.Profile:
                    {
                        var command = new ProfileCommentCommand.DeleteCommentCommand(request.CommentId);
                        return await _mediator.Send(command);
                    }
                case CommentCategory.Group:
                    {
                        var command = new GroupCommentCommand.DeleteCommentCommand(request.CommentId);
                        return await _mediator.Send(command);
                    }
                default:
                    {
                        throw new NotFoundException("Comment", request.CommentId);
                    }
            }
        }
    }
}