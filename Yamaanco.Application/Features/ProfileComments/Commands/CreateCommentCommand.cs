using MediatR;
using Microsoft.AspNetCore.Http;
using Yamaanco.Application.ApiResponses;
using Yamaanco.Application.DTOs.Comment;

namespace Yamaanco.Application.Features.ProfileComments.Commands
{
    public class CreateCommentCommand : IRequest<Response<CommentDto>>
    {
        public IFormFileCollection Attachments { get; set; }
        public string Parent { get; set; }
        public string Root { get; set; }
        public string Content { get; set; }
        public string ProfileId { get; set; }
        public string[] Pings { get; set; }
    }
}