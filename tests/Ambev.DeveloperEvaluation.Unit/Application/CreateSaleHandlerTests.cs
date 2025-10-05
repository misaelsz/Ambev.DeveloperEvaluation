using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Application.TestData;
using AutoMapper;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application;

public class CreateSaleHandlerTests
{
    private readonly ISaleRepository _saleRepository;
    private readonly IMapper _mapper;
    private readonly CreateSaleHandler _handler;

    public CreateSaleHandlerTests()
    {
        _saleRepository = Substitute.For<ISaleRepository>();
        _mapper = Substitute.For<IMapper>();
        _handler = new CreateSaleHandler(_saleRepository, _mapper);
    }

    [Fact(DisplayName = "Given valid sale data When creating sale Then returns success response")]
    public async Task Handle_ValidRequest_ReturnsSuccessResponse()
    {
        var command = CreateSaleHandlerTestData.GenerateValidCommand();
        var sale = CreateSaleHandlerTestData.GenerateValidSale();
        var result = CreateSaleHandlerTestData.GenerateValidResult();

        _mapper.Map<Sale>(command).Returns(sale);
        _mapper.Map<CreateSaleResult>(sale).Returns(result);
        _saleRepository.CreateAsync(Arg.Any<Sale>(), Arg.Any<CancellationToken>())
            .Returns(sale);

        var createSaleResult = await _handler.Handle(command, CancellationToken.None);

        createSaleResult.Should().NotBeNull();
        createSaleResult.Id.Should().Be(result.Id);
        createSaleResult.SaleNumber.Should().Be(result.SaleNumber);
        createSaleResult.CustomerId.Should().Be(result.CustomerId);
        createSaleResult.CustomerName.Should().Be(result.CustomerName);
        createSaleResult.CustomerDocument.Should().Be(result.CustomerDocument);
        createSaleResult.BranchId.Should().Be(result.BranchId);
        createSaleResult.BranchName.Should().Be(result.BranchName);
        createSaleResult.SubtotalAmount.Should().Be(result.SubtotalAmount);
        createSaleResult.TotalDiscountAmount.Should().Be(result.TotalDiscountAmount);
        createSaleResult.TotalAmount.Should().Be(result.TotalAmount);
        createSaleResult.Status.Should().Be(result.Status);
        await _saleRepository.Received(1).CreateAsync(Arg.Any<Sale>(), Arg.Any<CancellationToken>());
    }
}