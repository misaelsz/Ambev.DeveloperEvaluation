using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Application.TestData;

public static class CreateSaleHandlerTestData
{
    private static readonly Faker<CreateSaleCommand> createSaleCommandFaker = new Faker<CreateSaleCommand>()
        .RuleFor(s => s.SaleNumber, f => f.Random.AlphaNumeric(10).ToUpper())
        .RuleFor(s => s.SaleDate, f => f.Date.Recent())
        .RuleFor(s => s.CustomerId, f => f.Random.Guid())
        .RuleFor(s => s.CustomerName, f => f.Person.FullName)
        .RuleFor(s => s.CustomerDocument, f => f.Random.Replace("###########"))
        .RuleFor(s => s.BranchId, f => f.Random.Guid())
        .RuleFor(s => s.BranchName, f => f.Company.CompanyName())
        .RuleFor(s => s.Status, f => f.PickRandom<SaleStatus>())
        .RuleFor(s => s.Itens, f => f.Make(2, () => new SaleItem
        {
            ProductId = f.Random.Guid(),
            Quantity = f.Random.Int(1, 5),
            UnitPrice = f.Random.Decimal(10, 100)
        }));

    private static readonly Faker<Sale> saleFaker = new Faker<Sale>()
        .RuleFor(s => s.Id, f => f.Random.Guid())
        .RuleFor(s => s.SaleNumber, f => f.Random.AlphaNumeric(10).ToUpper())
        .RuleFor(s => s.SaleDate, f => f.Date.Recent())
        .RuleFor(s => s.CustomerId, f => f.Random.Guid())
        .RuleFor(s => s.CustomerName, f => f.Person.FullName)
        .RuleFor(s => s.CustomerDocument, f => f.Random.Replace("###########"))
        .RuleFor(s => s.BranchId, f => f.Random.Guid())
        .RuleFor(s => s.BranchName, f => f.Company.CompanyName())
        .RuleFor(s => s.SubtotalAmount, f => f.Random.Decimal(0, 1000))
        .RuleFor(s => s.TotalDiscountAmount, f => f.Random.Decimal(0, 200))
        .RuleFor(s => s.TotalAmount, (f, s) => Math.Max(0, s.SubtotalAmount - s.TotalDiscountAmount))
        .RuleFor(s => s.Status, f => f.PickRandom<SaleStatus>())
        .RuleFor(s => s.IsCancelled, f => false)
        .RuleFor(s => s.CreatedAt, f => f.Date.Recent())
        .RuleFor(s => s.UpdatedAt, f => f.Date.Recent());

    private static readonly Faker<CreateSaleResult> createSaleResultFaker = new Faker<CreateSaleResult>()
        .RuleFor(r => r.Id, f => f.Random.Guid())
        .RuleFor(r => r.SaleNumber, f => f.Random.AlphaNumeric(10).ToUpper())
        .RuleFor(r => r.SaleDate, f => f.Date.Recent())
        .RuleFor(r => r.CustomerId, f => f.Random.Guid())
        .RuleFor(r => r.CustomerName, f => f.Person.FullName)
        .RuleFor(r => r.CustomerDocument, f => f.Random.Replace("###########"))
        .RuleFor(r => r.BranchId, f => f.Random.Guid())
        .RuleFor(r => r.BranchName, f => f.Company.CompanyName())
        .RuleFor(r => r.SubtotalAmount, f => f.Random.Decimal(0, 1000))
        .RuleFor(r => r.TotalDiscountAmount, f => f.Random.Decimal(0, 200))
        .RuleFor(r => r.TotalAmount, (f, r) => Math.Max(0, r.SubtotalAmount - r.TotalDiscountAmount))
        .RuleFor(r => r.Status, f => f.PickRandom<SaleStatus>());

    public static CreateSaleCommand GenerateValidCommand() => createSaleCommandFaker.Generate();
    public static Sale GenerateValidSale() => saleFaker.Generate();
    public static CreateSaleResult GenerateValidResult() => createSaleResultFaker.Generate();
}