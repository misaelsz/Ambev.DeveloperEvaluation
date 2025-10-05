using Ambev.DeveloperEvaluation.Application.Customers.CreateCustomer;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Application.TestData;
using AutoMapper;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application;

public class CreateCustomerHandlerTests
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IMapper _mapper;
    private readonly CreateCustomerHandler _handler;

    public CreateCustomerHandlerTests()
    {
        _customerRepository = Substitute.For<ICustomerRepository>();
        _mapper = Substitute.For<IMapper>();
        _handler = new CreateCustomerHandler(_customerRepository, _mapper);
    }

    [Fact(DisplayName = "Given valid customer data When creating customer Then returns success response")]
    public async Task Handle_ValidRequest_ReturnsSuccessResponse()
    {
        var command = CreateCustomerHandlerTestData.GenerateValidCommand();
        var customer = CreateCustomerHandlerTestData.GenerateValidCustomer();
        var result = CreateCustomerHandlerTestData.GenerateValidResult();

        _mapper.Map<Customer>(command).Returns(customer);
        _mapper.Map<CreateCustomerResult>(customer).Returns(result);
        _customerRepository.CreateAsync(Arg.Any<Customer>(), Arg.Any<CancellationToken>())
            .Returns(customer);

        var createCustomerResult = await _handler.Handle(command, CancellationToken.None);

        createCustomerResult.Should().NotBeNull();
        createCustomerResult.Id.Should().Be(result.Id);
        createCustomerResult.Name.Should().Be(result.Name);
        createCustomerResult.Email.Should().Be(result.Email);
        createCustomerResult.Phone.Should().Be(result.Phone);
        createCustomerResult.Document.Should().Be(result.Document);
        createCustomerResult.Address.Should().Be(result.Address);
        createCustomerResult.Status.Should().Be(result.Status);
        await _customerRepository.Received(1).CreateAsync(Arg.Any<Customer>(), Arg.Any<CancellationToken>());
    }
}