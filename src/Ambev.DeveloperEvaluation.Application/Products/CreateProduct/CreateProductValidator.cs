using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Products.CreateProduct;

public class CreateProductValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(200);

        RuleFor(x => x.Code)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(x => x.Description)
            .MaximumLength(1000);

        RuleFor(x => x.Category)
            .MaximumLength(100);

        RuleFor(x => x.UnitPrice)
            .GreaterThan(0);
    }
}