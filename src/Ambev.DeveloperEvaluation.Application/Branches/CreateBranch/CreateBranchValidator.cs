using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Branches.CreateBranch;

public class CreateBranchValidator : AbstractValidator<CreateBranchCommand>
{
    public CreateBranchValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(200);

        RuleFor(x => x.Code)
            .NotEmpty()
            .MaximumLength(20);

        RuleFor(x => x.Address)
            .MaximumLength(500);

        RuleFor(x => x.City)
            .MaximumLength(100);

        RuleFor(x => x.State)
            .MaximumLength(50);

        RuleFor(x => x.PostalCode)
            .MaximumLength(20);

        RuleFor(x => x.Phone)
            .MaximumLength(20);

        RuleFor(x => x.ManagerName)
            .MaximumLength(200);
    }
}