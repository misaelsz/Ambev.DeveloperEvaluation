using Ambev.DeveloperEvaluation.Application.Products.CreateProduct;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Application.TestData;
using AutoMapper;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application;

public class CreateProductHandlerTests
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;
    private readonly CreateProductHandler _handler;

    public CreateProductHandlerTests()
    {
        _productRepository = Substitute.For<IProductRepository>();
        _mapper = Substitute.For<IMapper>();
        _handler = new CreateProductHandler(_productRepository, _mapper);
    }

    [Fact(DisplayName = "Given valid product data When creating product Then returns success response")]
    public async Task Handle_ValidRequest_ReturnsSuccessResponse()
    {
        var command = CreateProductHandlerTestData.GenerateValidCommand();
        var product = CreateProductHandlerTestData.GenerateValidProduct();
        var result = CreateProductHandlerTestData.GenerateValidResult();

        _mapper.Map<Product>(command).Returns(product);
        _mapper.Map<CreateProductResult>(product).Returns(result);
        _productRepository.CreateAsync(Arg.Any<Product>(), Arg.Any<CancellationToken>())
            .Returns(product);

        var createProductResult = await _handler.Handle(command, CancellationToken.None);

        createProductResult.Should().NotBeNull();
        createProductResult.Id.Should().Be(result.Id);
        createProductResult.Name.Should().Be(result.Name);
        createProductResult.Code.Should().Be(result.Code);
        createProductResult.Description.Should().Be(result.Description);
        createProductResult.Category.Should().Be(result.Category);
        createProductResult.UnitPrice.Should().Be(result.UnitPrice);
        createProductResult.Status.Should().Be(result.Status);
        await _productRepository.Received(1).CreateAsync(Arg.Any<Product>(), Arg.Any<CancellationToken>());
    }
}