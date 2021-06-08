using FluentValidation;
using Yamaanco.Application.Features.Groups.Commands;

namespace Yamaanco.Application.Features.Groups.Validators
{
    public class UpdateGroupCommandValidator : AbstractValidator<UpdateGroupCommand>
    {
        public UpdateGroupCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.GroupTypeId).NotEmpty();
            RuleFor(x => x.Name).MinimumLength(2).MaximumLength(150).NotEmpty();
        }
    }
}