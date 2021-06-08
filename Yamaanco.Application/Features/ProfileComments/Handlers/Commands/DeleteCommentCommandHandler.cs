using MediatR;
using Microsoft.Extensions.Options;
using System;
using System.Threading;
using System.Threading.Tasks;
using Yamaanco.Application.ApiResponses;
using Yamaanco.Application.Common.Exceptions;
using Yamaanco.Application.Common.Options;
using Yamaanco.Application.Features.ProfileComments.Commands;
using Yamaanco.Application.Interfaces;
using Yamaanco.Domain.Entities.ProfileEntities;
using Yamaanco.Domain.Enums;

namespace Yamaanco.Application.Features.ProfileComments.Handlers.Commands
{
    public class DeleteCommentCommandHandler : IRequestHandler<DeleteCommentCommand, Response<string>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteCommentCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Response<string>> Handle(DeleteCommentCommand request, CancellationToken cancellationToken)
        {
            var comment = await _unitOfWork.ProfileCommentRepository
                         .SingleOrDefaultAsync(m => m.Id == request.CommentId);

            if (comment == null)
            {
                throw new NotFoundException(nameof(ProfileComment), request.CommentId);
            }

            comment.Delete();

            await _unitOfWork.CommitAsync();
            return new Response<string>(request.CommentId, "Comment deleted successfully.");
        }
    }
}