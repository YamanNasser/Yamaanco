using MediatR;
using Microsoft.AspNetCore.Http;
using Yamaanco.Application.ApiResponses;
using Yamaanco.Application.DTOs.Message;

namespace Yamaanco.Application.Features.GroupMessages.Commands
{
    public class CreateMessageCommand : IRequest<Response<MessageDto>>
    {
        public IFormFile File { get; set; }
        public string Content { get; set; }
        public string GroupId { get; set; }
    }
}