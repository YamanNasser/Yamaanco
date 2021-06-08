﻿using MediatR;
using Yamaanco.Application.ApiResponses;

namespace Yamaanco.Application.Features.Comments.Commands
{
    public class DeleteCommentCommand : IRequest<Response<string>>
    {
        public DeleteCommentCommand(string commentId)
        {
            CommentId = commentId;
        }

        public string CommentId { get; set; }
    }
}