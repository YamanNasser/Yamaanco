using AutoMapper.Internal;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Yamaanco.Application.Features.GroupMessages.Notifications;
using Yamaanco.Application.Interfaces;
using Yamaanco.Domain.Entities.GroupEntities;

namespace Yamaanco.Application.Features.GroupMessages.Handlers.Notifications
{
    public class MessagesReceivedHandler : INotificationHandler<MessagesReceived>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDateTime _dateTime;

        public MessagesReceivedHandler(IUnitOfWork unitOfWork,
            IDateTime dateTime)
        {
            _unitOfWork = unitOfWork;
            _dateTime = dateTime;
        }

        public async Task Handle(MessagesReceived notification, CancellationToken cancellationToken)
        {
            var currentLoadedMessages = notification.ReceivedResult.Select(o => o.Id);

            var theSeenMessagesInCurrentLoadedComment = _unitOfWork
                .GroupMessageViewerRepository
                .Find(pm => pm.ProfileId == notification.ViewerId &&
                      currentLoadedMessages.Contains(pm.MessageId))
                .Select(o => o.MessageId);

            var unSeenMessagesInCurrentLoadedComment = notification
                .ReceivedResult
                .Where(o => !theSeenMessagesInCurrentLoadedComment.Contains(o.Id))
                .Select(o => o.Id);

            //Update the received message view count, and add viewer to  message viewer list
            _unitOfWork.GroupMessageRepository
                 .Find(o => unSeenMessagesInCurrentLoadedComment.Contains(o.Id))
                 .ForAll(message =>
                 {
                     message.AddNewViewer();

                     _unitOfWork.GroupMessageViewerRepository
                         .Add(new GroupMessageViewer(
                              messageId: message.Id,
                              groupId: message.GroupId,
                             profileId: notification.ViewerId
                         ));
                 });

            //set the current comment notification as seen by viewer.
            _unitOfWork.GroupNotificationRepository
               .Find(o => unSeenMessagesInCurrentLoadedComment.Contains(o.SourceId))
               .ForAll(n => n.SetAsSeen());

            await _unitOfWork.CommitAsync(cancellationToken);
        }
    }
}