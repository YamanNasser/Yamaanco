using FluentValidation;
using Yamaanco.Application.Features.GroupFollowers.Commands;
using Yamaanco.Application.Interfaces;

namespace Yamaanco.Application.Features.GroupFollowers.Validators
{
    public class FollowProfileCommandValidator : AbstractValidator<FollowProfileCommand>
    {
        public FollowProfileCommandValidator(IAccountService accountService)
        {
            var currentUser = accountService.GetCurrentUser();
            RuleFor(x => x.ProfileId).NotEmpty();
            RuleFor(x => x.ProfileId).NotEqual(x => currentUser.Id);
        }
    }
}