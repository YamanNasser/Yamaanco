using FluentValidation;
using Yamaanco.Application.Features.Groups.Commands;

namespace Yamaanco.Application.Features.Groups.Validators
{
    public class CreateGroupCommandValidator : AbstractValidator<CreateGroupCommand>
    {
        public CreateGroupCommandValidator()
        {
            RuleFor(x => x.GroupTypeId).NotEmpty();
            RuleFor(x => x.Name).MinimumLength(2).MaximumLength(150).NotEmpty();
        }
    }
}