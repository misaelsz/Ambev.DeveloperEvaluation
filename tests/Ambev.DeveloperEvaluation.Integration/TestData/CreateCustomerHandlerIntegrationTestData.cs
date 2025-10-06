using Ambev.DeveloperEvaluation.Application.Customers.CreateCustomer;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Bogus;

namespace Ambev.DeveloperEvaluation.Integration.TestData;

public static class CreateCustomerHandlerIntegrationTestData
{
    private static readonly Faker<CreateCustomerCommand> createCustomerCommandFaker = new Faker<CreateCustomerCommand>()
        .RuleFor(c => c.Name, f => f.Person.FullName)
        .RuleFor(c => c.Email, f => f.Internet.Email())
        .RuleFor(c => c.Phone, f => f.Phone.PhoneNumber("(##) #####-####"))
        .RuleFor(c => c.Document, f => f.Random.Replace("###########"))
        .RuleFor(c => c.Address, f => f.Address.FullAddress())
        .RuleFor(c => c.Status, f => f.PickRandom<CustomerStatus>());

    public static CreateCustomerCommand GenerateValidCommand()
    {
        return createCustomerCommandFaker.Generate();
    }
}