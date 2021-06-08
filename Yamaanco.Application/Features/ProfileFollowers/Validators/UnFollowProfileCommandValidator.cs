using FluentValidation;
using Yamaanco.Application.Features.GroupFollowers.Commands;

namespace Yamaanco.Application.Features.GroupFollowers.Validators
{
    internal class UnFollowProfileCommandValidator : AbstractValidator<UnFollowProfileCommand>
    {
        public UnFollowProfileCommandValidator()
        {
            RuleFor(x => x.ProfileId).NotEmpty();
        }
    }
}