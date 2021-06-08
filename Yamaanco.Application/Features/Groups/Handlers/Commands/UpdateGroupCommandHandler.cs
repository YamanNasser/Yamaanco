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
    public class UpdateGroupCommandHandler : IRequestHandler<UpdateGroupCommand, Response<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IImageService _manageImage;
        private readonly AppOptions _appSettings;
        private readonly IAccountService _accountService;

        public UpdateGroupCommandHandler(IUnitOfWork unitOfWork, IImageService manageImage, IOptions<AppOptions> appSettings, IAccountService accountService)
        {
            _appSettings = appSettings.Value;
            _unitOfWork = unitOfWork;
            _manageImage = manageImage;
            _accountService = accountService;
        }

        public async Task<Response<string>> Handle(UpdateGroupCommand request, CancellationToken cancellationToken)
        {
            var currentUser = _accountService.GetCurrentUser();
            var group = await _unitOfWork.GroupRepository
                .SingleOrDefaultAsync(o => o.Id == request.Id);

            if (group == null)
            {
                throw new NotFoundException(nameof(Group), request.Id);
            }

            var isCurrentUserIsGroupAdmin = await _unitOfWork.GroupMemberRepository.AnyAsync(o => o.MemberId == currentUser.Id && o.GroupId == request.Id && o.IsAdmin);

            if (!isCurrentUserIsGroupAdmin)
            {
                throw new NotFoundException(nameof(Group), request.Id);
            }

            group.Update(request.Name, request.GroupTypeId, request.Description, currentUser.Id);

            var groupPhoto = _unitOfWork.GroupPhotoResourcesRepository
             .Find(o => o.GroupId == request.Id);

            if (request.Photo != null)
            {
                foreach (var photo in group.PhotoResources)
                {
                    photo.SetPath(_appSettings.GroupUploadFolderName);

                    var length = await _manageImage.Upload(request.Photo, photo.Path, group.Id, photo.PhotoSize);

                    photo.SetProperties(
                        extension: Path.GetExtension(request.Photo.FileName),
                        contentType: request.Photo.ContentType,
                        length: length
                        );
                }
            }

            await _unitOfWork.CommitAsync();
            return new Response<string>(group.Id, "Group updated successfully.");
        }
    }
}