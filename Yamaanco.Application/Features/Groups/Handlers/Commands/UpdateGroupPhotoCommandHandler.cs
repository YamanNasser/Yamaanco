using MediatR;
using Microsoft.Extensions.Options;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Yamaanco.Application.ApiResponses;
using Yamaanco.Application.Common.Exceptions;
using Yamaanco.Application.Common.Options;
using Yamaanco.Application.Features.Groups.Commands;
using Yamaanco.Application.Interfaces;
using Yamaanco.Domain.Entities.GroupEntities;

namespace Yamaanco.Application.Features.Groups.Handlers.Commands
{
    public class UpdateGroupPhotoCommandHandler : IRequestHandler<UpdateGroupPhotoCommand, Response<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IImageService _manageImage;
        private readonly AppOptions _appSettings;
        private readonly IAccountService _accountService;

        public UpdateGroupPhotoCommandHandler(IUnitOfWork unitOfWork, IImageService manageImage, IOptions<AppOptions> appSettings, IAccountService accountService)
        {
            _appSettings = appSettings.Value;
            _unitOfWork = unitOfWork;
            _manageImage = manageImage;
            _accountService = accountService;
        }

        public async Task<Response<string>> Handle(UpdateGroupPhotoCommand request, CancellationToken cancellationToken)
        {
            var currentUser = _accountService.GetCurrentUser();

            var entity = _unitOfWork.GroupPhotoResourcesRepository.Find(o => o.GroupId == request.Id);

            var isCurrentUserIsGroupAdmin = await _unitOfWork.GroupMemberRepository.AnyAsync(o => o.MemberId == currentUser.Id && o.GroupId == request.Id && o.IsAdmin);

            if (!isCurrentUserIsGroupAdmin)
            {
                throw new NotFoundException(nameof(Group), request.Id);
            }

            if (request.Photo != null && entity != null && entity.Count() >= 1)
            {
                foreach (var photo in entity)
                {
                    photo.SetPath(_appSettings.GroupUploadFolderName);

                    var length = await _manageImage.Upload(request.Photo, photo.Path, photo.GroupId, photo.PhotoSize);

                    photo.SetProperties(
                        extension: Path.GetExtension(request.Photo.FileName),
                        contentType: request.Photo.ContentType,
                        length: length
                        );
                }

                await _unitOfWork.CommitAsync();
                return new Response<string>(request.Id, "Group photo updated successfully.");
            }

            throw new NotFoundException(nameof(Group), request.Id);
        }
    }
}