using MediatR;
using Microsoft.Extensions.Options;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Yamaanco.Application.ApiResponses;
using Yamaanco.Application.Common.Options;
using Yamaanco.Application.DTOs.Message;
using Yamaanco.Application.Features.ProfileMessages.Commands;
using Yamaanco.Application.Features.ProfileMessages.Notifications;
using Yamaanco.Application.Interfaces;
using Yamaanco.Domain.Entities.ProfileEntities;
using Yamaanco.Domain.Enums;

namespace Yamaanco.Application.Features.ProfileMessages.Handlers.Commands
{
    public class CreateMessageCommandHandler : IRequestHandler<CreateMessageCommand, Response<MessageDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMediator _mediator;
        private readonly IFileService _fileService;
        private readonly AppOptions _appSettings;
        private readonly IDateTime _dateTime;
        private readonly IAccountService _accountService;

        public CreateMessageCommandHandler(IUnitOfWork unitOfWork, IFileService fileService, IOptions<AppOptions> appSettings, IMediator mediator, IDateTime dateTime, IAccountService accountService)
        {
            _appSettings = appSettings.Value;
            _unitOfWork = unitOfWork;
            _mediator = mediator;
            _fileService = fileService;
            _dateTime = dateTime;
            _accountService = accountService;
        }

        public async Task<Response<MessageDto>> Handle(CreateMessageCommand request, CancellationToken cancellationToken)
        {
            var currentUser = _accountService.GetCurrentUser();

            var message = new ProfileMessage(
                content: request.Content,
                profileId: request.ProfileId,
                createdById: currentUser.Id,
                messageType: MessageCategory.Profile
            );

            await AddAttachment(request, message);

            _unitOfWork.ProfileMessageRepository.Add(message);
            await _unitOfWork.CommitAsync();

            var createdMessage = await _unitOfWork.ProfileMessageRepository.GetProfileMessage(message.Id);

            await _mediator.Publish(
                   new MessageCreated
                   {
                       Message = createdMessage
                   },
               cancellationToken);

            return new Response<MessageDto>(createdMessage, "Message Created successfully.");
        }

        private async Task AddAttachment(CreateMessageCommand request, ProfileMessage message)
        {
            if (request.File != null)
            {
                var file = new ProfileMessageResources(_appSettings.ProfileMessageUploadFolderName, message.ProfileId, message.Id, message.Content);

                var length = await _fileService.Upload(request.File, file.Path, file.Id);

                file.SetProperties(
                    length: length,
                    contentType: request.File.ContentType,
                    extension: Path.GetExtension(request.File.FileName),
                    description: message.Content);

                message.AttachFile(file);
            }
        }
    }
}