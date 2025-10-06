using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.ORM.TestData;

public static class CustomerRepositoryTestData
{
    private static readonly Faker<Customer> customerFaker = new Faker<Customer>()
        .RuleFor(c => c.Name, f => f.Person.FullName)
        .RuleFor(c => c.Email, f => f.Internet.Email())
        .RuleFor(c => c.Phone, f => f.Phone.PhoneNumber("(##) #####-####"))
        .RuleFor(c => c.Document, f => f.Random.Replace("###########"))
        .RuleFor(c => c.Address, f => f.Address.FullAddress())
        .RuleFor(c => c.Status, f => f.PickRandom<CustomerStatus>())
        .RuleFor(c => c.CreatedAt, f => f.Date.Recent())
        .RuleFor(c => c.UpdatedAt, f => f.Date.Recent());

    public static Customer GenerateValidCustomer()
    {
        return customerFaker.Generate();
    }
}