using MediatR;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Yamaanco.Application.ApiResponses;
using Yamaanco.Application.Common.Options;
using Yamaanco.Application.DTOs.Comment;
using Yamaanco.Application.Extensions;
using Yamaanco.Application.Features.ProfileComments.Commands;
using Yamaanco.Application.Features.ProfileComments.Notifications;
using Yamaanco.Application.Interfaces;
using Yamaanco.Domain.Entities.ProfileEntities;
using Yamaanco.Domain.Enums;

namespace Yamaanco.Application.Features.ProfileComments.Handlers.Commands
{
    public class CreateCommentCommandHandler : IRequestHandler<CreateCommentCommand, Response<CommentDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMediator _mediator;
        private readonly IFileService _fileService;
        private readonly AppOptions _appSettings;
        private readonly IAccountService _accountService;

        public CreateCommentCommandHandler(IUnitOfWork unitOfWork, IFileService fileService, IOptions<AppOptions> appSettings, IMediator mediator, IAccountService accountService)
        {
            _appSettings = appSettings.Value;
            _unitOfWork = unitOfWork;
            _mediator = mediator;
            _fileService = fileService;
            _accountService = accountService;
        }

        public async Task<Response<CommentDto>> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
        {
            var currentUser = _accountService.GetCurrentUser();

            var comment = new ProfileComment(
                profileId: request.ProfileId,
                parent: request.Parent,
                root: request.Root,
                content: request.Content,
                category: CommentCategory.Profile,
                type: request.Root == null ? CommentType.Post : CommentType.Reply,
                classification: request.Attachments != null && request.Attachments.Count >= 1
               ? CommentClassification.WithAttachments : CommentClassification.Default,
                createdById: currentUser.Id,
                pings: request.Pings
                );

            var resourcer = await GetResources(request, comment);
            comment.AddResources(resourcer);

            _unitOfWork.ProfileCommentRepository.Add(comment);
            await _unitOfWork.CommitAsync();

            var createdComment = await _unitOfWork.ProfileCommentRepository.GetCommentWithoutReplies(comment.Id);

            await _mediator.Publish(
                 new CommentCreated
                 {
                     Comment = createdComment
                 },
             cancellationToken);

            return new Response<CommentDto>(createdComment, "Comment created successfully.");
        }

        private async Task<List<ProfileCommentResources>> GetResources(CreateCommentCommand request, ProfileComment comment)
        {
            var profileCommentResources = new List<ProfileCommentResources>();

            if (request.Attachments != null)
            {
                foreach (var attachment in request.Attachments)
                {
                    var commentResources = new ProfileCommentResources(
                           folderName: _appSettings.ProfileCommentUploadFolderName,
                           commentId: comment.Id,
                           profileId: comment.ProfileId,
                           description: comment.Content);

                    var length = await _fileService.Upload(attachment, commentResources.Path, commentResources.Id);

                    commentResources.SetProperties(length, attachment.ContentType,
                        Path.GetExtension(attachment.FileName));
                    profileCommentResources.Add(commentResources);
                }
            }

            return profileCommentResources;
        }
    }
}