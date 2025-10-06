using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Integration.TestData;
using Ambev.DeveloperEvaluation.ORM;
using Ambev.DeveloperEvaluation.ORM.Repositories;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Ambev.DeveloperEvaluation.Integration.ORM;

public class SaleRepositoryIntegrationTests : IDisposable
{
    private readonly DefaultContext _context;
    private readonly ISaleRepository _saleRepository;

    public SaleRepositoryIntegrationTests()
    {
        var options = new DbContextOptionsBuilder<DefaultContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new DefaultContext(options);
        _saleRepository = new SaleRepository(_context);
    }

    [Fact(DisplayName = "Given valid sale When creating Then persists and returns created sale")]
    public async Task CreateAsync_ValidSale_PersistsAndReturnsCreatedSale()
    {
        var sale = SaleRepositoryIntegrationTestData.GenerateValidSale();

        var result = await _saleRepository.CreateAsync(sale, CancellationToken.None);

        result.Should().NotBeNull();
        result.Id.Should().Be(sale.Id);
        result.SaleNumber.Should().Be(sale.SaleNumber);
        result.CustomerName.Should().Be(sale.CustomerName);
        result.BranchName.Should().Be(sale.BranchName);
        result.TotalAmount.Should().Be(sale.TotalAmount);
        result.Status.Should().Be(sale.Status);

        var persistedSale = await _context.Sales.FindAsync(result.Id);
        persistedSale.Should().NotBeNull();
        persistedSale!.SaleNumber.Should().Be(sale.SaleNumber);
        persistedSale.CustomerName.Should().Be(sale.CustomerName);
        persistedSale.BranchName.Should().Be(sale.BranchName);
        persistedSale.TotalAmount.Should().Be(sale.TotalAmount);
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}