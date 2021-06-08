using MediatR;
using Microsoft.AspNetCore.Http;
using Yamaanco.Application.ApiResponses;

namespace Yamaanco.Application.Features.GroupComments.Commands
{
    public class UpdateCommentCommand : IRequest<Response<string>>
    {
        public IFormFileCollection Attachments { get; set; }
        public string CommentId { get; set; }
        public string Content { get; set; }
        public string[] Pings { get; set; }
    }
}