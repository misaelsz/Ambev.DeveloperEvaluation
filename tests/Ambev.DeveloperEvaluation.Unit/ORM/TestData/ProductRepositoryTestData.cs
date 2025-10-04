using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.ORM.TestData;

public static class ProductRepositoryTestData
{
    private static readonly Faker<Product> productFaker = new Faker<Product>()
        .RuleFor(p => p.Name, f => f.Commerce.ProductName())
        .RuleFor(p => p.Code, f => f.Commerce.Ean13())
        .RuleFor(p => p.Description, f => f.Commerce.ProductDescription())
        .RuleFor(p => p.Category, f => f.Commerce.Categories(1).First())
        .RuleFor(p => p.UnitPrice, f => f.Random.Decimal(10, 500))
        .RuleFor(p => p.Status, f => f.PickRandom<ProductStatus>())
        .RuleFor(p => p.CreatedAt, f => f.Date.Recent())
        .RuleFor(p => p.UpdatedAt, f => f.Date.Recent());

    public static Product GenerateValidProduct()
    {
        return productFaker.Generate();
    }
}