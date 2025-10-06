using Ambev.DeveloperEvaluation.Application.Customers.CreateCustomer;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Application.TestData;

public static class CreateCustomerHandlerTestData
{
    private static readonly Faker<CreateCustomerCommand> createCustomerCommandFaker = new Faker<CreateCustomerCommand>()
        .RuleFor(c => c.Name, f => f.Person.FullName)
        .RuleFor(c => c.Email, f => f.Internet.Email())
        .RuleFor(c => c.Phone, f => f.Phone.PhoneNumber("(##) #####-####"))
        .RuleFor(c => c.Document, f => f.Random.Replace("###########"))
        .RuleFor(c => c.Address, f => f.Address.FullAddress())
        .RuleFor(c => c.Status, f => f.PickRandom<CustomerStatus>());

    private static readonly Faker<Customer> customerFaker = new Faker<Customer>()
        .RuleFor(c => c.Id, f => f.Random.Guid())
        .RuleFor(c => c.Name, f => f.Person.FullName)
        .RuleFor(c => c.Email, f => f.Internet.Email())
        .RuleFor(c => c.Phone, f => f.Phone.PhoneNumber("(##) #####-####"))
        .RuleFor(c => c.Document, f => f.Random.Replace("###########"))
        .RuleFor(c => c.Address, f => f.Address.FullAddress())
        .RuleFor(c => c.Status, f => f.PickRandom<CustomerStatus>())
        .RuleFor(c => c.CreatedAt, f => f.Date.Recent())
        .RuleFor(c => c.UpdatedAt, f => f.Date.Recent());

    private static readonly Faker<CreateCustomerResult> createCustomerResultFaker = new Faker<CreateCustomerResult>()
        .RuleFor(r => r.Id, f => f.Random.Guid())
        .RuleFor(r => r.Name, f => f.Person.FullName)
        .RuleFor(r => r.Email, f => f.Internet.Email())
        .RuleFor(r => r.Phone, f => f.Phone.PhoneNumber("(##) #####-####"))
        .RuleFor(r => r.Document, f => f.Random.Replace("###########"))
        .RuleFor(r => r.Address, f => f.Address.FullAddress())
        .RuleFor(r => r.Status, f => f.PickRandom<CustomerStatus>());

    public static CreateCustomerCommand GenerateValidCommand()
    {
        return createCustomerCommandFaker.Generate();
    }

    public static Customer GenerateValidCustomer()
    {
        return customerFaker.Generate();
    }

    public static CreateCustomerResult GenerateValidResult()
    {
        return createCustomerResultFaker.Generate();
    }
}