using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Bogus;

namespace Ambev.DeveloperEvaluation.Integration.TestData;

public static class BranchRepositoryIntegrationTestData
{
    private static readonly Faker<Branch> branchFaker = new Faker<Branch>()
        .RuleFor(b => b.Name, f => f.Company.CompanyName())
        .RuleFor(b => b.Code, f => f.Random.AlphaNumeric(6).ToUpper())
        .RuleFor(b => b.Address, f => f.Address.StreetAddress())
        .RuleFor(b => b.City, f => f.Address.City())
        .RuleFor(b => b.State, f => f.Address.StateAbbr())
        .RuleFor(b => b.PostalCode, f => f.Address.ZipCode())
        .RuleFor(b => b.Phone, f => f.Phone.PhoneNumber("(##) ####-####"))
        .RuleFor(b => b.ManagerName, f => f.Person.FullName)
        .RuleFor(b => b.Status, f => f.PickRandom<BranchStatus>())
        .RuleFor(b => b.CreatedAt, f => f.Date.Recent())
        .RuleFor(b => b.UpdatedAt, f => f.Date.Recent());

    public static Branch GenerateValidBranch()
    {
        return branchFaker.Generate();
    }
}