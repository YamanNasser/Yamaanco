using AutoMapper.Internal;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Yamaanco.Application.Features.ProfileMessages.Notifications;
using Yamaanco.Application.Interfaces;
using Yamaanco.Domain.Entities.ProfileEntities;

namespace Yamaanco.Application.Features.ProfileMessages.Handlers.Notifications
{
    public class MessagesReceivedHandler : INotificationHandler<MessagesReceived>
    {
        private readonly IUnitOfWork _unitOfWork;

        public MessagesReceivedHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(MessagesReceived notification, CancellationToken cancellationToken)
        {
            var currentLoadedMessages = notification.ReceivedResult.Select(o => o.Id);

            var theSeenMessagesInCurrentLoadedComment = _unitOfWork
                .ProfileMessageViewerRepository
                .Find(pm => pm.ProfileId == notification.ViewerId &&
                      currentLoadedMessages.Contains(pm.MessageId))
                .Select(o => o.MessageId);

            var unSeenMessagesInCurrentLoadedComment = notification
                .ReceivedResult
                .Where(o => !theSeenMessagesInCurrentLoadedComment.Contains(o.Id))
                .Select(o => o.Id);

            //Update the received message view count, and add viewer to  message viewer list
            _unitOfWork.ProfileMessageRepository
                 .Find(o => unSeenMessagesInCurrentLoadedComment.Contains(o.Id))
                 .ForAll(message =>
                 {
                     message.AddNewViewer();

                     _unitOfWork.ProfileMessageViewerRepository
                         .Add(new ProfileMessageViewer(
                              messageId: message.Id,
                             profileId: notification.ViewerId
                         ));
                 });

            //set the current comment notification as seen by viewer.
            _unitOfWork.ProfileNotificationRepository
               .Find(o => unSeenMessagesInCurrentLoadedComment
               .Contains(o.SourceId))
               .ForAll(n => n.SetAsSeen());

            await _unitOfWork.CommitAsync(cancellationToken);
        }
    }
}