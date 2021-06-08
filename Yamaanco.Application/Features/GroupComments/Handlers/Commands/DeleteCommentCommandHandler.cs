using MediatR;
using Microsoft.Extensions.Options;
using System.Threading;
using System.Threading.Tasks;
using Yamaanco.Application.ApiResponses;
using Yamaanco.Application.Common.Exceptions;
using Yamaanco.Application.Common.Options;
using Yamaanco.Application.Features.GroupComments.Commands;
using Yamaanco.Application.Interfaces;
using Yamaanco.Domain.Entities.GroupEntities;

namespace Yamaanco.Application.Features.GroupComments.Handlers.Commands
{
    public class DeleteCommentCommandHandler : IRequestHandler<DeleteCommentCommand, Response<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileService _fileService;
        private readonly AppOptions _appSettings;

        public DeleteCommentCommandHandler(IUnitOfWork unitOfWork, IFileService fileService, IOptions<AppOptions> appSettings)
        {
            _appSettings = appSettings.Value;
            _unitOfWork = unitOfWork;
            _fileService = fileService;
        }

        public async Task<Response<string>> Handle(DeleteCommentCommand request, CancellationToken cancellationToken)
        {
            var comment = await _unitOfWork.GroupCommentRepository
                         .SingleOrDefaultAsync(m => m.Id == request.CommentId);

            if (comment == null)
            {
                throw new NotFoundException(nameof(GroupComment), request.CommentId);
            }

            comment.Delete();

            await _unitOfWork.CommitAsync();
            return new Response<string>(request.CommentId, "Comment deleted successfully.");
        }
    }
}