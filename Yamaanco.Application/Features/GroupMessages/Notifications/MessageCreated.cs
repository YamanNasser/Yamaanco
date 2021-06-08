using MediatR;
using Yamaanco.Application.DTOs.Message;

namespace Yamaanco.Application.Features.GroupMessages.Notifications
{
    public class MessageCreated : INotification
    {
        public MessageDto Message { get; set; }
    }
}