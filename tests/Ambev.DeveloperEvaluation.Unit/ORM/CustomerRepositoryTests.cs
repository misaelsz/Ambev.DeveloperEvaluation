using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.ORM.TestData;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.ORM;

public class CustomerRepositoryTests
{
    [Fact(DisplayName = "Given valid customer When creating Then returns created customer")]
    public async Task CreateAsync_ValidCustomer_ReturnsCreatedCustomer()
    {
        var customerRepository = Substitute.For<ICustomerRepository>();
        var customer = CustomerRepositoryTestData.GenerateValidCustomer();

        customerRepository.CreateAsync(Arg.Any<Customer>(), Arg.Any<CancellationToken>())
            .Returns(customer);

        var result = await customerRepository.CreateAsync(customer, CancellationToken.None);

        result.Should().NotBeNull();
        result.Id.Should().Be(customer.Id);
        result.Name.Should().Be(customer.Name);
        result.Email.Should().Be(customer.Email);
        result.Phone.Should().Be(customer.Phone);
        result.Document.Should().Be(customer.Document);
        result.Address.Should().Be(customer.Address);
        result.Status.Should().Be(customer.Status);
        await customerRepository.Received(1).CreateAsync(Arg.Any<Customer>(), Arg.Any<CancellationToken>());
    }
}