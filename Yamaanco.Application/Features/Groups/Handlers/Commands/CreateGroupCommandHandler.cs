using MediatR;
using Microsoft.Extensions.Options;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Yamaanco.Application.ApiResponses;
using Yamaanco.Application.Common.Options;
using Yamaanco.Application.Features.Groups.Commands;
using Yamaanco.Application.Interfaces;
using Yamaanco.Domain.Entities.GroupEntities;
using Yamaanco.Domain.Enums;

namespace Yamaanco.Application.Features.Groups.Handlers.Commands
{
    public class CreateGroupCommandHandler : IRequestHandler<CreateGroupCommand, Response<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IImageService _manageImage;
        private readonly AppOptions _appSettings;
        private readonly IAccountService _accountService;
        private readonly IDateTime _dateTime;

        public CreateGroupCommandHandler(IUnitOfWork unitOfWork, IImageService manageImage, IOptions<AppOptions> appSettings, IAccountService accountService, IDateTime dateTime)
        {
            _appSettings = appSettings.Value;
            _unitOfWork = unitOfWork;
            _manageImage = manageImage;
            _accountService = accountService;
            _dateTime = dateTime;
        }

        public async Task<Response<string>> Handle(CreateGroupCommand request, CancellationToken cancellationToken)
        {
            var currentUser = _accountService.GetCurrentUser();

            var group = new Group(
                name: request.Name,
                description: request.Description,
                groupTypeId: request.GroupTypeId,
                createdById: currentUser.Id);

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

            _unitOfWork.GroupRepository.Add(group);

            await _unitOfWork.CommitAsync();
            return new Response<string>(group.Id, "Group created successfully.");
        }
    }
}