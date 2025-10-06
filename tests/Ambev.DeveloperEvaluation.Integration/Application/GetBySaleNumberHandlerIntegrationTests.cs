using Ambev.DeveloperEvaluation.Application.Branches.CreateBranch;
using Ambev.DeveloperEvaluation.Application.Customers.CreateCustomer;
using Ambev.DeveloperEvaluation.Application.Products.CreateProduct;
using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Application.Sales.GetBySaleNumber;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.ORM;
using Ambev.DeveloperEvaluation.ORM.Repositories;
using AutoMapper;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Ambev.DeveloperEvaluation.Integration.Application;

public class GetBySaleNumberHandlerIntegrationTests : IDisposable
{
    private readonly DefaultContext _context;
    private readonly ISaleRepository _saleRepository;
    private readonly IBranchRepository _branchRepository;
    private readonly ICustomerRepository _customerRepository;
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;
    private readonly GetBySaleNumberHandler _handler;
    private readonly CreateSaleHandler _createSaleHandler;
    private readonly CreateBranchHandler _createBranchHandler;
    private readonly CreateCustomerHandler _createCustomerHandler;
    private readonly CreateProductHandler _createProductHandler;

    public GetBySaleNumberHandlerIntegrationTests()
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
            cfg.AddProfile<GetBySaleNumberProfile>();
            cfg.AddProfile<CreateBranchProfile>();
            cfg.AddProfile<CreateCustomerProfile>();
            cfg.AddProfile<CreateProductProfile>();
        });
        _mapper = config.CreateMapper();

        _handler = new GetBySaleNumberHandler(_saleRepository, _mapper);
        _createSaleHandler = new CreateSaleHandler(_saleRepository, _mapper, _branchRepository, _customerRepository, _productRepository);
        _createBranchHandler = new CreateBranchHandler(_branchRepository, _mapper);
        _createCustomerHandler = new CreateCustomerHandler(_customerRepository, _mapper);
        _createProductHandler = new CreateProductHandler(_productRepository, _mapper);
    }

    [Fact(DisplayName = "Given existing sale When querying by sale number Then returns sale data")]
    public async Task Handle_ValidSaleNumber_ReturnsExistingSale()
    {
        var saleNumber = "SALE-12345";

        var branchResult = await _createBranchHandler.Handle(new CreateBranchCommand
        {
            Name = "Main Branch",
            Code = "MB-001",
            Address = "Av. Central, 100",
            City = "Sao Paulo",
            State = "SP",
            PostalCode = "01000-000",
            Phone = "(11) 1111-1111",
            ManagerName = "Manager",
            Status = Domain.Enums.BranchStatus.Active
        }, CancellationToken.None);

        var customerResult = await _createCustomerHandler.Handle(new CreateCustomerCommand
        {
            Name = "John Doe",
            Email = "john.doe@example.com",
            Phone = "(11) 99999-9999",
            Document = "12345678901",
            Address = "Rua A, 200",
            Status = Domain.Enums.CustomerStatus.Active
        }, CancellationToken.None);

        var productResult = await _createProductHandler.Handle(new CreateProductCommand
        {
            Name = "Product X",
            Code = "PRD001",
            Description = "Desc",
            Category = "Cat",
            UnitPrice = 50,
            Status = Domain.Enums.ProductStatus.Active
        }, CancellationToken.None);

        var createCommand = new CreateSaleCommand
        {
            SaleNumber = saleNumber,
            SaleDate = DateTime.UtcNow,
            CustomerId = customerResult.Id,
            CustomerName = customerResult.Name,
            CustomerDocument = customerResult.Document,
            BranchId = branchResult.Id,
            BranchName = branchResult.Name,
            Status = Domain.Enums.SaleStatus.Confirmed,
            Itens = new List<Domain.Entities.SaleItem>
            {
                new Domain.Entities.SaleItem
                {
                    ProductId = productResult.Id,
                    Quantity = 2,
                    UnitPrice = productResult.UnitPrice
                }
            }
        };
        await _createSaleHandler.Handle(createCommand, CancellationToken.None);

        var command = new GetBySaleNumberCommand { SaleNumber = saleNumber };

        var result = await _handler.Handle(command, CancellationToken.None);

        result.Should().NotBeNull();
        result!.SaleNumber.Should().Be(saleNumber);
        result.CustomerId.Should().Be(customerResult.Id);
        result.BranchId.Should().Be(branchResult.Id);
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}