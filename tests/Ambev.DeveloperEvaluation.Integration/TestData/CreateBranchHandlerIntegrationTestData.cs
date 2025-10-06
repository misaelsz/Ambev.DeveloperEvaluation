using Ambev.DeveloperEvaluation.Application.Branches.CreateBranch;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Bogus;

namespace Ambev.DeveloperEvaluation.Integration.TestData;

public static class CreateBranchHandlerIntegrationTestData
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

    public static CreateBranchCommand GenerateValidCommand()
    {
        return createBranchCommandFaker.Generate();
    }
}