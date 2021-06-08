using FluentValidation;
using Yamaanco.Application.Features.Profiles.Commands;

namespace Yamaanco.Application.Features.Profiles.Validators
{
    public class CreateProfileCommandValidator : AbstractValidator<CreateProfileCommand>
    {
        public CreateProfileCommandValidator()
        {
            RuleFor(x => x.GenderId).NotEmpty();
            RuleFor(x => x.Address).MaximumLength(200);
            RuleFor(x => x.City).MaximumLength(100);
            RuleFor(x => x.Country).MaximumLength(100);

            //RuleFor(x => x.FirstName).MinimumLength(2).MaximumLength(60).NotEmpty();
            //RuleFor(x => x.LastName).MinimumLength(2).MaximumLength(60).NotEmpty();
            //RuleFor(x => x.Email).EmailAddress().NotEmpty();
            //RuleFor(x => x.PhoneNumber).MaximumLength(24);
            //RuleFor(x => x.Password).NotEmpty().MinimumLength(6);
            //RuleFor(x => x.ConfirmPassword).NotEmpty().Equal(x => x.Password);
        }
    }
}