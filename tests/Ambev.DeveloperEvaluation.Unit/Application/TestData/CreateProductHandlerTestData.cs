using Ambev.DeveloperEvaluation.Application.Products.CreateProduct;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Application.TestData;

public static class CreateProductHandlerTestData
{
    private static readonly Faker<CreateProductCommand> createProductCommandFaker = new Faker<CreateProductCommand>()
        .RuleFor(p => p.Name, f => f.Commerce.ProductName())
        .RuleFor(p => p.Code, f => f.Commerce.Ean13())
        .RuleFor(p => p.Description, f => f.Commerce.ProductDescription())
        .RuleFor(p => p.Category, f => f.Commerce.Categories(1).First())
        .RuleFor(p => p.UnitPrice, f => f.Random.Decimal(10, 500))
        .RuleFor(p => p.Status, f => f.PickRandom<ProductStatus>());

    private static readonly Faker<Product> productFaker = new Faker<Product>()
        .RuleFor(p => p.Id, f => f.Random.Guid())
        .RuleFor(p => p.Name, f => f.Commerce.ProductName())
        .RuleFor(p => p.Code, f => f.Commerce.Ean13())
        .RuleFor(p => p.Description, f => f.Commerce.ProductDescription())
        .RuleFor(p => p.Category, f => f.Commerce.Categories(1).First())
        .RuleFor(p => p.UnitPrice, f => f.Random.Decimal(10, 500))
        .RuleFor(p => p.Status, f => f.PickRandom<ProductStatus>())
        .RuleFor(p => p.CreatedAt, f => f.Date.Recent())
        .RuleFor(p => p.UpdatedAt, f => f.Date.Recent());

    private static readonly Faker<CreateProductResult> createProductResultFaker = new Faker<CreateProductResult>()
        .RuleFor(r => r.Id, f => f.Random.Guid())
        .RuleFor(r => r.Name, f => f.Commerce.ProductName())
        .RuleFor(r => r.Code, f => f.Commerce.Ean13())
        .RuleFor(r => r.Description, f => f.Commerce.ProductDescription())
        .RuleFor(r => r.Category, f => f.Commerce.Categories(1).First())
        .RuleFor(r => r.UnitPrice, f => f.Random.Decimal(10, 500))
        .RuleFor(r => r.Status, f => f.PickRandom<ProductStatus>());

    public static CreateProductCommand GenerateValidCommand()
    {
        return createProductCommandFaker.Generate();
    }

    public static Product GenerateValidProduct()
    {
        return productFaker.Generate();
    }

    public static CreateProductResult GenerateValidResult()
    {
        return createProductResultFaker.Generate();
    }
}