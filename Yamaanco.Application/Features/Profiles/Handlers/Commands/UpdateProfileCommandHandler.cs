using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using Yamaanco.Application.ApiResponses;
using Yamaanco.Application.Common.Exceptions;
using Yamaanco.Application.Features.Profiles.Commands;
using Yamaanco.Application.Interfaces;
using Yamaanco.Domain.Entities.ProfileEntities;

namespace Yamaanco.Application.Features.Profiles.Handlers.Commands
{
    public class UpdateProfileCommandHandler : IRequestHandler<UpdateProfileCommand, Response<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAccountService _accountService;

        public UpdateProfileCommandHandler(IUnitOfWork unitOfWork, IAccountService accountService)
        {
            _unitOfWork = unitOfWork;
            _accountService = accountService;
        }

        public async Task<Response<string>> Handle(UpdateProfileCommand request, CancellationToken cancellationToken)
        {
            var currentUser = _accountService.GetCurrentUser();

            var profile = await _unitOfWork.ProfileRepository
                .SingleAsync(o => o.Id == request.Id);

            if (profile == null)
                throw new NotFoundException(nameof(Profile), request.Id);

            //Only User Profile Owner can update his/her profile information
            if (profile.CreatedById != currentUser.Id)
                throw new NotFoundException(nameof(Profile), request.Id);

            profile.Update(
               firstName: currentUser.FirstName,
               lastName: currentUser.LastName,
               genderId: request.GenderId,
               birthDate: Convert.ToDateTime(request.BirthDate),
               phoneNumber: currentUser.PhoneNumber,
               email: currentUser.Email,
               country: request.Country,
               city: request.City,
               address: request.Address,
               aboutMe: request.AboutMe);

            await _accountService.UpdateAccount(request.Id, request.FirstName, request.LastName, request.Email, request.PhoneNumber);

            await _unitOfWork.CommitAsync();

            return new Response<string>(profile.Id, message: $"Profile updated successfully.");
        }
    }
}