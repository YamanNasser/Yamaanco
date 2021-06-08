using MediatR;
using Microsoft.Extensions.Options;
using System;
using System.Threading;
using System.Threading.Tasks;
using Yamaanco.Application.ApiResponses;
using Yamaanco.Application.Common.Options;
using Yamaanco.Application.DTOs.Account;
using Yamaanco.Application.Features.Profiles.Commands;
using Yamaanco.Application.Interfaces;
using Yamaanco.Domain.Entities.ProfileEntities;
using Yamaanco.Domain.Enums;
using Profile = Yamaanco.Domain.Entities.ProfileEntities.Profile;

namespace Yamaanco.Application.Features.Profiles.Handlers.Commands
{
    public class CreateProfileCommandHandler : IRequestHandler<CreateProfileCommand, Response<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAccountService _accountService;

        public CreateProfileCommandHandler(IUnitOfWork unitOfWork, IAccountService accountService)
        {
            _unitOfWork = unitOfWork;
            _accountService = accountService;
        }

        public async Task<Response<string>> Handle(CreateProfileCommand request, CancellationToken cancellationToken)
        {
            var currentUser = _accountService.GetCurrentUser();

            var profile = new Profile(
               id: currentUser.Id,
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

            _unitOfWork.ProfileRepository.Add(profile);
            await _unitOfWork.CommitAsync();

            return new Response<string>(profile.Id, "Profile successfully created");
        }
    }
}