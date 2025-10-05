using Ambev.DeveloperEvaluation.Application.Products.CreateProduct;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Bogus;

namespace Ambev.DeveloperEvaluation.Integration.TestData;

public static class CreateProductHandlerIntegrationTestData
{
    private static readonly Faker<CreateProductCommand> createProductCommandFaker = new Faker<CreateProductCommand>()
        .RuleFor(p => p.Name, f => f.Commerce.ProductName())
        .RuleFor(p => p.Code, f => f.Commerce.Ean13())
        .RuleFor(p => p.Description, f => f.Commerce.ProductDescription())
        .RuleFor(p => p.Category, f => f.Commerce.Categories(1).First())
        .RuleFor(p => p.UnitPrice, f => f.Random.Decimal(10, 500))
        .RuleFor(p => p.Status, f => f.PickRandom<ProductStatus>());

    public static CreateProductCommand GenerateValidCommand()
    {
        return createProductCommandFaker.Generate();
    }
}