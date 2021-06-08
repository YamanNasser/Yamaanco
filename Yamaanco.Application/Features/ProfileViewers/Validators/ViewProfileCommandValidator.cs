using FluentValidation;
using Yamaanco.Application.Features.ProfileViewers.Commands;

namespace Yamaanco.Application.Features.ProfileViewers.Validators
{
    public class ViewProfileCommandValidator : AbstractValidator<ViewProfileCommand>
    {
        public ViewProfileCommandValidator()
        {
            RuleFor(x => x.ProfileId).NotEmpty();
        }
    }
}