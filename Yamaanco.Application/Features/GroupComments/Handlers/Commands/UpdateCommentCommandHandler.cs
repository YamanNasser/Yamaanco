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
using Yamaanco.Application.Common.Options;
using Yamaanco.Application.DTOs.Comment;
using Yamaanco.Application.DTOs.Group;
using Yamaanco.Application.Extensions;
using Yamaanco.Application.Features.GroupComments.Commands;
using Yamaanco.Application.Features.GroupComments.Notifications;
using Yamaanco.Application.Interfaces;
using Yamaanco.Domain.Entities.GroupEntities;
using Yamaanco.Domain.Enums;

namespace Yamaanco.Application.Features.GroupComments.Handlers.Commands
{
    public class UpdateCommentCommandHandler : IRequestHandler<UpdateCommentCommand, Response<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;
        private readonly AppOptions _appSettings;
        private readonly IAccountService _accountService;

        public UpdateCommentCommandHandler(IUnitOfWork unitOfWork, IFileService fileService, IOptions<AppOptions> appSettings, IMediator mediator, IMapper mapper, IAccountService accountService)
        {
            _appSettings = appSettings.Value;
            _unitOfWork = unitOfWork;
            _mediator = mediator;
            _fileService = fileService;
            _mapper = mapper;
            _accountService = accountService;
        }

        public async Task<Response<string>> Handle(UpdateCommentCommand request, CancellationToken cancellationToken)
        {
            var currentUser = _accountService.GetCurrentUser();
            var comment = await _unitOfWork.GroupCommentRepository.GetGroupComment(request.CommentId);

            if (comment == null || string.IsNullOrEmpty(comment?.Id))
            {
                throw new NotFoundException(nameof(GroupComment), request.CommentId);
            }

            var newResourcer = await GetNewResources(request, comment);

            comment.Update(
                classification: request.Attachments != null && request.Attachments.Count >= 1 ? CommentClassification.WithAttachments : CommentClassification.Default,
                content: request.Content,
                 lastModifiedById: currentUser.Id,
                pings: request.Pings,
                resources: newResourcer);

            await _unitOfWork.CommitAsync();

            await SendNotification(comment, cancellationToken);
            return new Response<string>(comment.Id, "Comment updated successfully.");
        }

        private async Task SendNotification(GroupComment comment, CancellationToken cancellationToken)
        {
            var updatedComment = await _unitOfWork
                .GroupCommentRepository
                .GetCommentWithoutReplies(comment.Id);

            var attachments = MapResourcesAsCommentResourcesDto(comment.CommentResources);

            await _mediator.Publish(
                   new CommentUpdated
                   {
                       Id = updatedComment.Id,
                       Parent = updatedComment.Parent,
                       Root = updatedComment.Root,
                       Content = updatedComment.Content,
                       Attachments = attachments,
                       UpdatedById = updatedComment.CreatedById,
                       Pings = updatedComment.Pings.Select(o => o.UserId).ToArray(),
                       CategoryId = updatedComment.CategoryId,
                       CategoryName = updatedComment.CategoryName,
                       UpdatedByName = updatedComment.CreatorName
                   },
               cancellationToken);
        }

        private static List<CommentResourcesDto> MapResourcesAsCommentResourcesDto(IEnumerable<GroupCommentResources> resources)
        {
            var commentResources = new List<CommentResourcesDto>();
            foreach (var recource in resources)
            {
                commentResources.Add(new CommentResourcesDto()
                {
                    CategoryId = recource.GroupId,
                    CommentId = recource.CommentId,
                    ContentType = recource.ContentType,
                    Description = recource.Description,
                    Id = recource.Id,
                    Length = recource.Length,
                    Path = recource.Path
                });
            }

            return commentResources;
        }

        private async Task<List<GroupCommentResources>> GetNewResources(UpdateCommentCommand request, GroupComment comment)
        {
            var groupCommentResources = new List<GroupCommentResources>();

            if (request.Attachments != null)
            {
                foreach (var attachment in request.Attachments)
                {
                    var commentResources = new GroupCommentResources(
                           folderName: _appSettings.GroupCommentUploadFolderName,
                           commentId: comment.Id,
                           groupId: comment.GroupId,
                           description: comment.Content);

                    var length = await _fileService.Upload(attachment, commentResources.Path, commentResources.Id);

                    commentResources.SetProperties(length, attachment.ContentType,
                        Path.GetExtension(attachment.FileName));
                    groupCommentResources.Add(commentResources);
                }
            }

            return groupCommentResources;
        }
    }
}