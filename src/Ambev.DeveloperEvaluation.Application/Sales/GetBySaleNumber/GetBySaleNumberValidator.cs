using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetBySaleNumber;

public class GetBySaleNumberValidator : AbstractValidator<GetBySaleNumberCommand>
{
    public GetBySaleNumberValidator()
    {
        RuleFor(x => x.SaleNumber)
            .NotEmpty()
            .MaximumLength(50);
    }
}