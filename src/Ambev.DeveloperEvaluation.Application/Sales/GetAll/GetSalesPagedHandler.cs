using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetAll;

public class GetSalesPagedHandler : IRequestHandler<GetSalesPagedQuery, IEnumerable<GetSalesListItemResult>>
{
    private readonly ISaleRepository _saleRepository;
    private readonly IMapper _mapper;

    public GetSalesPagedHandler(ISaleRepository saleRepository, IMapper mapper)
    {
        _saleRepository = saleRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<GetSalesListItemResult>> Handle(GetSalesPagedQuery request, CancellationToken cancellationToken)
    {
        var data = await _saleRepository.GetAllAsync(request.Page, request.PageSize, cancellationToken);
        return _mapper.Map<IEnumerable<GetSalesListItemResult>>(data);
    }
}
