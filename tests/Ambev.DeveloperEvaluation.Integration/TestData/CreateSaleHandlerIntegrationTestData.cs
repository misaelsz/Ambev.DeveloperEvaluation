using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Bogus;

namespace Ambev.DeveloperEvaluation.Integration.TestData;

public static class CreateSaleHandlerIntegrationTestData
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
        .RuleFor(s => s.Itens, f => f.Make(3, () => new SaleItem
        {
            ProductId = f.Random.Guid(),
            Quantity = f.Random.Int(1, 10),
            UnitPrice = f.Random.Decimal(1, 100)
        }));

    public static CreateSaleCommand GenerateValidCommand() => createSaleCommandFaker.Generate();
}