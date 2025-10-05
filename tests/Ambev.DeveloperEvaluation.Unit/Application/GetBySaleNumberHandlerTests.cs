using Ambev.DeveloperEvaluation.Application.Sales.GetBySaleNumber;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Application.TestData;
using AutoMapper;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application;

public class GetBySaleNumberHandlerTests
{
    private readonly ISaleRepository _saleRepository;
    private readonly IMapper _mapper;
    private readonly GetBySaleNumberHandler _handler;

    public GetBySaleNumberHandlerTests()
    {
        _saleRepository = Substitute.For<ISaleRepository>();
        _mapper = Substitute.For<IMapper>();
        _handler = new GetBySaleNumberHandler(_saleRepository, _mapper);
    }

    [Fact(DisplayName = "Given valid sale number When handling Then returns sale result")]
    public async Task Handle_ValidSaleNumber_ReturnsSaleResult()
    {
        var command = GetBySaleNumberHandlerTestData.GenerateValidCommand();
        var sale = GetBySaleNumberHandlerTestData.GenerateValidSale();
        var result = GetBySaleNumberHandlerTestData.GenerateValidResult();

        _saleRepository.GetBySaleNumberAsync(command.SaleNumber, Arg.Any<CancellationToken>()).Returns(sale);
        _mapper.Map<GetBySaleNumberResult>(sale).Returns(result);

        var handlerResult = await _handler.Handle(command, CancellationToken.None);

        handlerResult.Should().NotBeNull();
        handlerResult.Id.Should().Be(result.Id);
        handlerResult.SaleNumber.Should().Be(result.SaleNumber);
        handlerResult.CustomerId.Should().Be(result.CustomerId);
        handlerResult.CustomerName.Should().Be(result.CustomerName);
        handlerResult.CustomerDocument.Should().Be(result.CustomerDocument);
        handlerResult.BranchId.Should().Be(result.BranchId);
        handlerResult.BranchName.Should().Be(result.BranchName);
        handlerResult.SubtotalAmount.Should().Be(result.SubtotalAmount);
        handlerResult.TotalDiscountAmount.Should().Be(result.TotalDiscountAmount);
        handlerResult.TotalAmount.Should().Be(result.TotalAmount);
    }
}