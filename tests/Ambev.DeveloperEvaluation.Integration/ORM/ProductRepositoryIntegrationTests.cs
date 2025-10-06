using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Integration.TestData;
using Ambev.DeveloperEvaluation.ORM;
using Ambev.DeveloperEvaluation.ORM.Repositories;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Ambev.DeveloperEvaluation.Integration.ORM;

public class ProductRepositoryIntegrationTests : IDisposable
{
    private readonly DefaultContext _context;
    private readonly IProductRepository _productRepository;

    public ProductRepositoryIntegrationTests()
    {
        var options = new DbContextOptionsBuilder<DefaultContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new DefaultContext(options);
        _productRepository = new ProductRepository(_context);
    }

    [Fact(DisplayName = "Given valid product When creating Then persists and returns created product")]
    public async Task CreateAsync_ValidProduct_PersistsAndReturnsCreatedProduct()
    {
        var product = ProductRepositoryIntegrationTestData.GenerateValidProduct();

        var result = await _productRepository.CreateAsync(product, CancellationToken.None);

        result.Should().NotBeNull();
        result.Id.Should().Be(product.Id);
        result.Name.Should().Be(product.Name);
        result.Code.Should().Be(product.Code);
        result.Description.Should().Be(product.Description);
        result.Category.Should().Be(product.Category);
        result.UnitPrice.Should().Be(product.UnitPrice);
        result.Status.Should().Be(product.Status);

        var persistedProduct = await _context.Products.FindAsync(result.Id);
        persistedProduct.Should().NotBeNull();
        persistedProduct!.Name.Should().Be(product.Name);
        persistedProduct.Code.Should().Be(product.Code);
        persistedProduct.Description.Should().Be(product.Description);
        persistedProduct.Category.Should().Be(product.Category);
        persistedProduct.UnitPrice.Should().Be(product.UnitPrice);
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}