using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.ORM.TestData;

public static class SaleRepositoryTestData
{
    private static readonly Faker<Sale> saleFaker = new Faker<Sale>()
        .RuleFor(s => s.SaleNumber, f => f.Commerce.Ean13())
        .RuleFor(s => s.SaleDate, f => f.Date.Recent())
        .RuleFor(s => s.CustomerId, f => f.Random.Guid())
        .RuleFor(s => s.CustomerName, f => f.Person.FullName)
        .RuleFor(s => s.CustomerDocument, f => f.Random.Replace("###########"))
        .RuleFor(s => s.BranchId, f => f.Random.Guid())
        .RuleFor(s => s.BranchName, f => f.Company.CompanyName())
        .RuleFor(s => s.SubtotalAmount, f => f.Random.Decimal(100, 1000))
        .RuleFor(s => s.TotalDiscountAmount, f => f.Random.Decimal(0, 100))
        .RuleFor(s => s.TotalAmount, f => f.Random.Decimal(100, 900))
        .RuleFor(s => s.Status, f => f.PickRandom<SaleStatus>())
        .RuleFor(s => s.IsCancelled, f => false)
        .RuleFor(s => s.CreatedAt, f => f.Date.Recent())
        .RuleFor(s => s.UpdatedAt, f => f.Date.Recent());

    public static Sale GenerateValidSale()
    {
        return saleFaker.Generate();
    }
}