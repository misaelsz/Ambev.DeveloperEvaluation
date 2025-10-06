using Ambev.DeveloperEvaluation.Application.Customers.CreateCustomer;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Integration.TestData;
using Ambev.DeveloperEvaluation.ORM;
using Ambev.DeveloperEvaluation.ORM.Repositories;
using AutoMapper;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Ambev.DeveloperEvaluation.Integration.Application;

public class CreateCustomerHandlerIntegrationTests : IDisposable
{
    private readonly DefaultContext _context;
    private readonly ICustomerRepository _customerRepository;
    private readonly IMapper _mapper;
    private readonly CreateCustomerHandler _handler;

    public CreateCustomerHandlerIntegrationTests()
    {
        var options = new DbContextOptionsBuilder<DefaultContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new DefaultContext(options);
        _customerRepository = new CustomerRepository(_context);

        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<CreateCustomerProfile>();
        });
        _mapper = config.CreateMapper();

        _handler = new CreateCustomerHandler(_customerRepository, _mapper);
    }

    [Fact(DisplayName = "Given valid command When handling Then persists and returns created customer")]
    public async Task Handle_ValidCommand_PersistsAndReturnsCreatedCustomer()
    {
        var command = CreateCustomerHandlerIntegrationTestData.GenerateValidCommand();

        var result = await _handler.Handle(command, CancellationToken.None);

        result.Should().NotBeNull();
        result.Id.Should().NotBeEmpty();
        result.Name.Should().Be(command.Name);
        result.Email.Should().Be(command.Email);
        result.Phone.Should().Be(command.Phone);
        result.Document.Should().Be(command.Document);
        result.Address.Should().Be(command.Address);
        result.Status.Should().Be(command.Status);

        var persistedCustomer = await _context.Customers.FindAsync(result.Id);
        persistedCustomer.Should().NotBeNull();
        persistedCustomer!.Name.Should().Be(command.Name);
        persistedCustomer.Email.Should().Be(command.Email);
        persistedCustomer.Phone.Should().Be(command.Phone);
        persistedCustomer.Document.Should().Be(command.Document);
        persistedCustomer.Address.Should().Be(command.Address);
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}