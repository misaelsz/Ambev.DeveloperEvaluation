using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.ORM.TestData;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.ORM;

public class SaleRepositoryTests
{
    [Fact(DisplayName = "Given valid sale When creating Then returns created sale")]
    public async Task CreateAsync_ValidSale_ReturnsCreatedSale()
    {
        var saleRepository = Substitute.For<ISaleRepository>();
        var sale = SaleRepositoryTestData.GenerateValidSale();

        saleRepository.CreateAsync(Arg.Any<Sale>(), Arg.Any<CancellationToken>())
            .Returns(sale);

        var result = await saleRepository.CreateAsync(sale, CancellationToken.None);

        result.Should().NotBeNull();
        result.Id.Should().Be(sale.Id);
        result.SaleNumber.Should().Be(sale.SaleNumber);
        result.CustomerName.Should().Be(sale.CustomerName);
        result.BranchName.Should().Be(sale.BranchName);
        result.TotalAmount.Should().Be(sale.TotalAmount);
        result.Status.Should().Be(sale.Status);
        await saleRepository.Received(1).CreateAsync(Arg.Any<Sale>(), Arg.Any<CancellationToken>());
    }

    [Fact(DisplayName = "Given valid id When getting sale Then returns found sale")]
    public async Task GetByIdAsync_ValidId_ReturnsFoundSale()
    {
        var saleRepository = Substitute.For<ISaleRepository>();
        var sale = SaleRepositoryTestData.GenerateValidSale();
        var saleId = sale.Id;

        saleRepository.GetByIdAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>())
            .Returns(sale);

        var result = await saleRepository.GetByIdAsync(saleId, CancellationToken.None);

        result.Should().NotBeNull();
        result!.Id.Should().Be(saleId);
        result.SaleNumber.Should().Be(sale.SaleNumber);
        result.CustomerName.Should().Be(sale.CustomerName);
        result.BranchName.Should().Be(sale.BranchName);
        result.TotalAmount.Should().Be(sale.TotalAmount);
        result.Status.Should().Be(sale.Status);
        await saleRepository.Received(1).GetByIdAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>());
    }

    [Fact(DisplayName = "Given valid sale number When getting sale Then returns found sale")]
    public async Task GetBySaleNumberAsync_ValidSaleNumber_ReturnsFoundSale()
    {
        var saleRepository = Substitute.For<ISaleRepository>();
        var sale = SaleRepositoryTestData.GenerateValidSale();
        var saleNumber = sale.SaleNumber;

        saleRepository.GetBySaleNumberAsync(Arg.Any<string>(), Arg.Any<CancellationToken>())
            .Returns(sale);

        var result = await saleRepository.GetBySaleNumberAsync(saleNumber, CancellationToken.None);

        result.Should().NotBeNull();
        result!.SaleNumber.Should().Be(saleNumber);
        result.Id.Should().Be(sale.Id);
        result.CustomerName.Should().Be(sale.CustomerName);
        result.BranchName.Should().Be(sale.BranchName);
        result.TotalAmount.Should().Be(sale.TotalAmount);
        result.Status.Should().Be(sale.Status);
        await saleRepository.Received(1).GetBySaleNumberAsync(Arg.Any<string>(), Arg.Any<CancellationToken>());
    }
}