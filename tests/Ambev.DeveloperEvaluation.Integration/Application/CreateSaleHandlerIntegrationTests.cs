using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
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
    private readonly IBranchRepository _branchRepository;
    private readonly ICustomerRepository _customerRepository;
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;
    private readonly CreateSaleHandler _handler;

    public CreateSaleHandlerIntegrationTests()
    {
        var options = new DbContextOptionsBuilder<DefaultContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new DefaultContext(options);
        _saleRepository = new SaleRepository(_context);
        _branchRepository = new BranchRepository(_context);
        _customerRepository = new CustomerRepository(_context);
        _productRepository = new ProductRepository(_context);

        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<CreateSaleProfile>();
        });
        _mapper = config.CreateMapper();

        _handler = new CreateSaleHandler(_saleRepository, _mapper, _branchRepository, _customerRepository, _productRepository);
    }

    [Fact(DisplayName = "Given valid command When handling Then persists and returns created sale")]
    public async Task Handle_ValidCommand_PersistsAndReturnsCreatedSale()
    {
        var command = CreateSaleHandlerIntegrationTestData.GenerateValidCommand();

        var branch = new Branch
        {
            Id = command.BranchId,
            Name = string.IsNullOrWhiteSpace(command.BranchName) ? "Branch" : command.BranchName,
            Code = "BR" + Guid.NewGuid().ToString("N").Substring(0, 6),
            Address = "Addr",
            City = "City",
            State = "ST",
            PostalCode = "00000-000",
            Phone = "0000",
            ManagerName = "Manager",
            Status = BranchStatus.Active
        };
        _context.Branches.Add(branch);

        var customer = new Customer
        {
            Id = command.CustomerId,
            Name = string.IsNullOrWhiteSpace(command.CustomerName) ? "Customer" : command.CustomerName,
            Email = "customer@example.com",
            Phone = "0000",
            Document = string.IsNullOrWhiteSpace(command.CustomerDocument) ? "00000000000" : command.CustomerDocument,
            Address = "Addr",
            Status = CustomerStatus.Active
        };
        _context.Customers.Add(customer);

        foreach (var group in command.Itens.GroupBy(i => i.ProductId))
        {
            var firstItem = group.First();
            var product = new Product
            {
                Id = firstItem.ProductId,
                Name = "Product",
                Code = "PRD" + Guid.NewGuid().ToString("N").Substring(0, 5),
                Description = "Desc",
                Category = "Cat",
                UnitPrice = firstItem.UnitPrice,
                Status = ProductStatus.Active
            };
            _context.Products.Add(product);
        }

        await _context.SaveChangesAsync();

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