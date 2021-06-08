using MediatR;
using System.Collections.Generic;
using Yamaanco.Application.DTOs.Message;

namespace Yamaanco.Application.Features.ProfileMessages.Notifications
{
    public class MessagesReceived : INotification
    {
        public IList<MessageDto> ReceivedResult { get; set; }
        public string ViewerId { get; set; }
    }
}