using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.ORM.TestData;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.ORM;

public class ProductRepositoryTests
{
    [Fact(DisplayName = "Given valid product When creating Then returns created product")]
    public async Task CreateAsync_ValidProduct_ReturnsCreatedProduct()
    {
        var productRepository = Substitute.For<IProductRepository>();
        var product = ProductRepositoryTestData.GenerateValidProduct();

        productRepository.CreateAsync(Arg.Any<Product>(), Arg.Any<CancellationToken>())
            .Returns(product);

        var result = await productRepository.CreateAsync(product, CancellationToken.None);

        result.Should().NotBeNull();
        result.Id.Should().Be(product.Id);
        result.Name.Should().Be(product.Name);
        result.Code.Should().Be(product.Code);
        result.Description.Should().Be(product.Description);
        result.Category.Should().Be(product.Category);
        result.UnitPrice.Should().Be(product.UnitPrice);
        result.Status.Should().Be(product.Status);
        await productRepository.Received(1).CreateAsync(Arg.Any<Product>(), Arg.Any<CancellationToken>());
    }
}