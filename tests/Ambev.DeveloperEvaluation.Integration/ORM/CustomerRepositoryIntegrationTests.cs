using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Integration.TestData;
using Ambev.DeveloperEvaluation.ORM;
using Ambev.DeveloperEvaluation.ORM.Repositories;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Ambev.DeveloperEvaluation.Integration.ORM;

public class CustomerRepositoryIntegrationTests : IDisposable
{
    private readonly DefaultContext _context;
    private readonly ICustomerRepository _customerRepository;

    public CustomerRepositoryIntegrationTests()
    {
        var options = new DbContextOptionsBuilder<DefaultContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new DefaultContext(options);
        _customerRepository = new CustomerRepository(_context);
    }

    [Fact(DisplayName = "Given valid customer When creating Then persists and returns created customer")]
    public async Task CreateAsync_ValidCustomer_PersistsAndReturnsCreatedCustomer()
    {
        var customer = CustomerRepositoryIntegrationTestData.GenerateValidCustomer();

        var result = await _customerRepository.CreateAsync(customer, CancellationToken.None);

        result.Should().NotBeNull();
        result.Id.Should().Be(customer.Id);
        result.Name.Should().Be(customer.Name);
        result.Email.Should().Be(customer.Email);
        result.Phone.Should().Be(customer.Phone);
        result.Document.Should().Be(customer.Document);
        result.Address.Should().Be(customer.Address);
        result.Status.Should().Be(customer.Status);

        var persistedCustomer = await _context.Customers.FindAsync(result.Id);
        persistedCustomer.Should().NotBeNull();
        persistedCustomer!.Name.Should().Be(customer.Name);
        persistedCustomer.Email.Should().Be(customer.Email);
        persistedCustomer.Phone.Should().Be(customer.Phone);
        persistedCustomer.Document.Should().Be(customer.Document);
        persistedCustomer.Address.Should().Be(customer.Address);
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}