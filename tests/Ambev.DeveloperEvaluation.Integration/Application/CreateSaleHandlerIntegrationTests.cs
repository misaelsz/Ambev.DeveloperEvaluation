using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Integration.TestData;
using Ambev.DeveloperEvaluation.ORM;
using Ambev.DeveloperEvaluation.ORM.Repositories;
using AutoMapper;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Ambev.DeveloperEvaluation.Integration.Application;

public class CreateSaleHandlerIntegrationTests : IDisposable
{
    private readonly DefaultContext _context;
    private readonly ISaleRepository _saleRepository;
    private readonly IMapper _mapper;
    private readonly CreateSaleHandler _handler;

    public CreateSaleHandlerIntegrationTests()
    {
        var options = new DbContextOptionsBuilder<DefaultContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new DefaultContext(options);
        _saleRepository = new SaleRepository(_context);

        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<CreateSaleProfile>();
        });
        _mapper = config.CreateMapper();

        _handler = new CreateSaleHandler(_saleRepository, _mapper);
    }

    [Fact(DisplayName = "Given valid command When handling Then persists and returns created sale")]
    public async Task Handle_ValidCommand_PersistsAndReturnsCreatedSale()
    {
        var command = CreateSaleHandlerIntegrationTestData.GenerateValidCommand();

        var result = await _handler.Handle(command, CancellationToken.None);

        result.Should().NotBeNull();
        result.Id.Should().NotBeEmpty();
        result.SaleNumber.Should().Be(command.SaleNumber);
        result.SaleDate.Should().Be(command.SaleDate);
        result.CustomerId.Should().Be(command.CustomerId);
        result.CustomerName.Should().Be(command.CustomerName);
        result.CustomerDocument.Should().Be(command.CustomerDocument);
        result.BranchId.Should().Be(command.BranchId);
        result.BranchName.Should().Be(command.BranchName);
        result.Status.Should().Be(command.Status);

        var persistedSale = await _context.Sales.FindAsync(result.Id);
        persistedSale.Should().NotBeNull();
        persistedSale!.SaleNumber.Should().Be(command.SaleNumber);
        persistedSale.SaleDate.Should().Be(command.SaleDate);
        persistedSale.CustomerId.Should().Be(command.CustomerId);
        persistedSale.CustomerName.Should().Be(command.CustomerName);
        persistedSale.CustomerDocument.Should().Be(command.CustomerDocument);
        persistedSale.BranchId.Should().Be(command.BranchId);
        persistedSale.BranchName.Should().Be(command.BranchName);
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}