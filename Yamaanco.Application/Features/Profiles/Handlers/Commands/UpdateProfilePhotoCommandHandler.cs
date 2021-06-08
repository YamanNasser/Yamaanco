using MediatR;
using Microsoft.Extensions.Options;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Yamaanco.Application.ApiResponses;
using Yamaanco.Application.Common.Exceptions;
using Yamaanco.Application.Common.Options;
using Yamaanco.Application.Features.Profiles.Commands;
using Yamaanco.Application.Interfaces;
using Yamaanco.Domain.Entities.ProfileEntities;

namespace Yamaanco.Application.Features.Profiles.Handlers.Commands
{
    public class UpdateProfilePhotoCommandHandler : IRequestHandler<UpdateProfilePhotoCommand, Response<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IImageService _manageImage;
        private readonly AppOptions _appSettings;
        private readonly IAccountService _accountService;

        public UpdateProfilePhotoCommandHandler(IUnitOfWork unitOfWork, IImageService manageImage, IOptions<AppOptions> appSettings, IAccountService accountService)
        {
            _appSettings = appSettings.Value;
            _unitOfWork = unitOfWork;
            _manageImage = manageImage;
            _accountService = accountService;
        }

        public async Task<Response<string>> Handle(UpdateProfilePhotoCommand request, CancellationToken cancellationToken)
        {
            var currentUser = _accountService.GetCurrentUser();

            if (request.Id != currentUser.Id)
                throw new NotFoundException(nameof(Profile), request.Id);

            var entity = _unitOfWork.ProfilePhotoResourcesRepository.Find(o => o.ProfileId == request.Id);

            if (request.Photo != null && entity != null && entity.Count() >= 1)
            {
                foreach (var photo in entity)
                {
                    photo.SetPath(_appSettings.ProfileUploadFolderName);

                    var length = await _manageImage.Upload(request.Photo, photo.Path, request.Id, photo.PhotoSize);

                    photo.SetProperties(
                        extension: Path.GetExtension(request.Photo.FileName),
                        contentType: request.Photo.ContentType,
                        length: length
                        );
                }

                await _unitOfWork.CommitAsync();
                return new Response<string>(request.Id, message: $"Profile photo updated successfully.");
            }

            throw new NotFoundException(nameof(Profile), request.Id);
        }
    }
}