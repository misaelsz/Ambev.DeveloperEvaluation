using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;
using FluentValidation;
using FluentValidation.Results;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale;

public class CreateSaleHandler : IRequestHandler<CreateSaleCommand, CreateSaleResult>
{
    private readonly ISaleRepository _saleRepository;
    private readonly IBranchRepository _branchRepository;
    private readonly ICustomerRepository _customerRepository;
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public CreateSaleHandler(
        ISaleRepository saleRepository,
        IMapper mapper,
        IBranchRepository branchRepository,
        ICustomerRepository customerRepository,
        IProductRepository productRepository)
    {
        _saleRepository = saleRepository;
        _mapper = mapper;
        _branchRepository = branchRepository;
        _customerRepository = customerRepository;
        _productRepository = productRepository;
    }

    public async Task<CreateSaleResult> Handle(CreateSaleCommand command, CancellationToken cancellationToken)
    {
        var validator = new CreateSaleValidator();
        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var failures = new List<ValidationFailure>();

        var branch = await _branchRepository.GetByIdAsync(command.BranchId, cancellationToken);
        if (branch is null)
            failures.Add(new ValidationFailure("BranchId", $"BranchId Invalid value: {command.BranchId}"));

        var customer = await _customerRepository.GetByIdAsync(command.CustomerId, cancellationToken);
        if (customer is null)
            failures.Add(new ValidationFailure("CustomerId", $"CustomerId Invalid value: {command.CustomerId}"));

        if (command.Itens is not null && command.Itens.Count > 0)
        {
            var distinctProductIds = command.Itens.Select(i => i.ProductId).Distinct();
            foreach (var productId in distinctProductIds)
            {
                var product = await _productRepository.GetByIdAsync(productId, cancellationToken);
                if (product is null)
                    failures.Add(new ValidationFailure("ProductId", $"ProductId Invalid value: {productId}"));
            }
        }

        if (failures.Count > 0)
            throw new ValidationException(failures);

        var sale = _mapper.Map<Sale>(command);
        var createdSale = await _saleRepository.CreateAsync(sale, cancellationToken);
        
        return _mapper.Map<CreateSaleResult>(createdSale);
    }
}