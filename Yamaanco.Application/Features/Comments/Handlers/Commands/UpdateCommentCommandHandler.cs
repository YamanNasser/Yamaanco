using AutoMapper;
using MediatR;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
    public class UpdateCommentCommandHandler : IRequestHandler<UpdateCommentCommand, Response<string>>
    {
        private readonly ICommentsRepository _commentsRepository;
        private readonly IMediator _mediator;
        private readonly IAccountService _accountService;

        public UpdateCommentCommandHandler(ICommentsRepository commentsRepository, IMediator mediator, IAccountService accountService)
        {
            _commentsRepository = commentsRepository;
            _mediator = mediator;
            _accountService = accountService;
        }

        public async Task<Response<string>> Handle(UpdateCommentCommand request, CancellationToken cancellationToken)
        {
            var currentUser = _accountService.GetCurrentUser();

            var category = await _commentsRepository.GetCommentCategory(currentUser.Id, request.CommentId);

            switch (category)
            {
                case CommentCategory.Profile:
                    {
                        var command = new ProfileCommentCommand.UpdateCommentCommand()
                        {
                            CommentId = request.CommentId,
                            Attachments = request.Attachments,
                            Content = request.Content,
                            Pings = request.Pings
                        };
                        return await _mediator.Send(command);
                    }
                case CommentCategory.Group:
                    {
                        var command = new GroupCommentCommand.UpdateCommentCommand()
                        {
                            CommentId = request.CommentId,
                            Attachments = request.Attachments,
                            Content = request.Content,
                            Pings = request.Pings
                        };
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