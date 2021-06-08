using FluentValidation;
using Yamaanco.Application.Features.GroupViewers.Commands;

namespace Yamaanco.Application.Features.GroupViewers.Validators
{
    public class CreateGroupViewerCommandValidator : AbstractValidator<ViewGroupCommand>
    {
        public CreateGroupViewerCommandValidator()
        {
            RuleFor(x => x.GroupId).NotEmpty();
        }
    }
}