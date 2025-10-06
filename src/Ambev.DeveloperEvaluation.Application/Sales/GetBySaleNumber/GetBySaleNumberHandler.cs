using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetBySaleNumber;

public class GetBySaleNumberHandler : IRequestHandler<GetBySaleNumberCommand, GetBySaleNumberResult>
{
    private readonly ISaleRepository _saleRepository;
    private readonly IMapper _mapper;

    public GetBySaleNumberHandler(ISaleRepository saleRepository, IMapper mapper)
    {
        _saleRepository = saleRepository;
        _mapper = mapper;
    }

    public async Task<GetBySaleNumberResult> Handle(GetBySaleNumberCommand request, CancellationToken cancellationToken)
    {
        var validator = new GetBySaleNumberValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
            throw new FluentValidation.ValidationException(validationResult.Errors);

        var sale = await _saleRepository.GetBySaleNumberAsync(request.SaleNumber, cancellationToken);
        if (sale is null)
            return null;

        return _mapper.Map<GetBySaleNumberResult>(sale);
    }
}