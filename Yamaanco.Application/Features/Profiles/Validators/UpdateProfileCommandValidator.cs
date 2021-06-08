﻿using FluentValidation;
using Yamaanco.Application.Features.Profiles.Commands;

namespace Yamaanco.Application.Features.Profiles.Validators
{
    public class UpdateProfileCommandValidator : AbstractValidator<UpdateProfileCommand>
    {
        public UpdateProfileCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.GenderId).NotEmpty();
            RuleFor(x => x.FirstName).MinimumLength(2).MaximumLength(60).NotEmpty();
            RuleFor(x => x.LastName).MinimumLength(2).MaximumLength(60).NotEmpty();
            RuleFor(x => x.Email).EmailAddress().NotEmpty();
            RuleFor(x => x.Address).MaximumLength(200);
            RuleFor(x => x.City).MaximumLength(100);
            RuleFor(x => x.Country).MaximumLength(100);
            RuleFor(x => x.PhoneNumber).MaximumLength(24);
        }
    }
}