using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetBySaleNumber;

public class GetBySaleNumberCommand : IRequest<GetBySaleNumberResult>
{
    public string SaleNumber { get; set; } = string.Empty;
}