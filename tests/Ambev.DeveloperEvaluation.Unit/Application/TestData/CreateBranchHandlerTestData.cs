using Ambev.DeveloperEvaluation.Application.Branches.CreateBranch;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Application.TestData;

public static class CreateBranchHandlerTestData
{
    private static readonly Faker<CreateBranchCommand> createBranchCommandFaker = new Faker<CreateBranchCommand>()
        .RuleFor(b => b.Name, f => f.Company.CompanyName())
        .RuleFor(b => b.Code, f => f.Random.AlphaNumeric(6).ToUpper())
        .RuleFor(b => b.Address, f => f.Address.StreetAddress())
        .RuleFor(b => b.City, f => f.Address.City())
        .RuleFor(b => b.State, f => f.Address.StateAbbr())
        .RuleFor(b => b.PostalCode, f => f.Address.ZipCode())
        .RuleFor(b => b.Phone, f => f.Phone.PhoneNumber("(##) ####-####"))
        .RuleFor(b => b.ManagerName, f => f.Person.FullName)
        .RuleFor(b => b.Status, f => f.PickRandom<BranchStatus>());

    private static readonly Faker<Branch> branchFaker = new Faker<Branch>()
        .RuleFor(b => b.Id, f => f.Random.Guid())
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

    private static readonly Faker<CreateBranchResult> createBranchResultFaker = new Faker<CreateBranchResult>()
        .RuleFor(r => r.Id, f => f.Random.Guid())
        .RuleFor(r => r.Name, f => f.Company.CompanyName())
        .RuleFor(r => r.Code, f => f.Random.AlphaNumeric(6).ToUpper())
        .RuleFor(r => r.Address, f => f.Address.StreetAddress())
        .RuleFor(r => r.City, f => f.Address.City())
        .RuleFor(r => r.State, f => f.Address.StateAbbr())
        .RuleFor(r => r.PostalCode, f => f.Address.ZipCode())
        .RuleFor(r => r.Phone, f => f.Phone.PhoneNumber("(##) ####-####"))
        .RuleFor(r => r.ManagerName, f => f.Person.FullName)
        .RuleFor(r => r.Status, f => f.PickRandom<BranchStatus>());

    public static CreateBranchCommand GenerateValidCommand()
    {
        return createBranchCommandFaker.Generate();
    }

    public static Branch GenerateValidBranch()
    {
        return branchFaker.Generate();
    }

    public static CreateBranchResult GenerateValidResult()
    {
        return createBranchResultFaker.Generate();
    }
}