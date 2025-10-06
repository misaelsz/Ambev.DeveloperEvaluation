using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;
using FluentValidation;
using FluentValidation.Results;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;

public class UpdateSaleHandler : IRequestHandler<UpdateSaleCommand, UpdateSaleResult>
{
    private readonly ISaleRepository _saleRepository;
    private readonly IBranchRepository _branchRepository;
    private readonly ICustomerRepository _customerRepository;
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public UpdateSaleHandler(ISaleRepository saleRepository, IMapper mapper, IBranchRepository branchRepository, ICustomerRepository customerRepository, IProductRepository productRepository)
    {
        _saleRepository = saleRepository;
        _mapper = mapper;
        _branchRepository = branchRepository;
        _customerRepository = customerRepository;
        _productRepository = productRepository;
    }

    public async Task<UpdateSaleResult> Handle(UpdateSaleCommand command, CancellationToken cancellationToken)
    {
        var failures = new List<ValidationFailure>();

        var sale = await _saleRepository.GetByIdAsync(command.Id, cancellationToken);
        if (sale is null)
            throw new ValidationException(new[] { new ValidationFailure("Id", $"Invalid value: {command.Id}") });

        var branch = await _branchRepository.GetByIdAsync(command.BranchId, cancellationToken);
        if (branch is null)
            failures.Add(new ValidationFailure("BranchId", $"Invalid value: {command.BranchId}"));

        var customer = await _customerRepository.GetByIdAsync(command.CustomerId, cancellationToken);
        if (customer is null)
            failures.Add(new ValidationFailure("CustomerId", $"Invalid value: {command.CustomerId}"));

        foreach (var productId in command.Itens.Select(i => i.ProductId).Distinct())
        {
            var product = await _productRepository.GetByIdAsync(productId, cancellationToken);
            if (product is null)
                failures.Add(new ValidationFailure("ProductId", $"Invalid value: {productId}"));
        }

        if (failures.Count > 0)
            throw new ValidationException(failures);

        sale.SaleNumber = command.SaleNumber;
        sale.SaleDate = command.SaleDate;
        sale.CustomerId = command.CustomerId;
        sale.CustomerName = command.CustomerName;
        sale.CustomerDocument = command.CustomerDocument;
        sale.BranchId = command.BranchId;
        sale.BranchName = command.BranchName;
        sale.Items = command.Itens;

        var updated = await _saleRepository.UpdateAsync(sale, cancellationToken);
        return _mapper.Map<UpdateSaleResult>(updated);
    }
}
