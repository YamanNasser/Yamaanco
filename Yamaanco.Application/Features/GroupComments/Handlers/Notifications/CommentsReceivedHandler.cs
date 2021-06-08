using AutoMapper.Internal;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Yamaanco.Application.Features.GroupComments.Notifications;
using Yamaanco.Application.Interfaces;
using Yamaanco.Domain.Entities.GroupEntities;
using Yamaanco.Domain.Enums;

namespace Yamaanco.Application.Features.GroupComments.Handlers.Notifications
{
    public class CommentsReceivedHandler : INotificationHandler<CommentsReceived>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDateTime _dateTime;

        public CommentsReceivedHandler(IUnitOfWork unitOfWork,
            IDateTime dateTime)
        {
            _unitOfWork = unitOfWork;
            _dateTime = dateTime;
        }

        //TODO: check performance
        public async Task Handle(CommentsReceived notification, CancellationToken cancellationToken)
        {
            var currentLoadedComment = notification.ReceivedResult
                              .Where(c => c.Root == null)
                              .Select(c => c.Id)
                              .ToList();

            var theSeenCommentsInCurrentLoadedComment = _unitOfWork
                .GroupCommentViewerRepository
                .Find(pcv => pcv.GroupId == notification.ViewerId &&
                      currentLoadedComment.Contains(pcv.CommentId))
                .Select(o => o.CommentId);

            var unSeenCommentsInCurrentLoadedComment = notification
                .ReceivedResult
                .Where(o => o.Root == null &&
                !theSeenCommentsInCurrentLoadedComment.Contains(o.Id))
                .Select(o => o.Id);

            //Update the received comment view count, and add viewer to  comment viewer list
            _unitOfWork.GroupCommentRepository
                 .Find(o => o.Root == null && unSeenCommentsInCurrentLoadedComment
                 .Contains(o.Id))
                 .ForAll(comment =>
                 {
                     comment.ViewCount += 1;

                     comment.CommentViewer
                         .Add(new GroupCommentViewer()
                         {
                             Id = Guid.NewGuid().ToString(),
                             CommentId = comment.Id,
                             Date = _dateTime.Now,
                             GroupId = comment.GroupId,
                             ViewerProfileId = notification.ViewerId
                         });
                 });

            //set the current comment notification as seen by viewer.
            _unitOfWork.GroupNotificationRepository
               .Find(o => unSeenCommentsInCurrentLoadedComment
               .Contains(o.SourceId))
               .ForAll(commentNotification => commentNotification.IsSeen = true);

            foreach (var comment in notification.ReceivedResult)
            {
                _unitOfWork.GroupCommentTransactionRepository.Add(
                       new GroupCommentTransaction()
                       {
                           CommentId = comment.Id,
                           CommentRoot = comment.Root,
                           CommentParent = comment.Parent,
                           Data = comment.Content,
                           GroupId = comment.CategoryId,
                           CommentTransactionType = CommentTransactionType.View,
                           TimeStamp = _dateTime.Now,
                           UserId = comment.CreatedById,
                           Id = Guid.NewGuid().ToString()
                       });
            }

            await _unitOfWork.CommitAsync(cancellationToken);
        }
    }
}