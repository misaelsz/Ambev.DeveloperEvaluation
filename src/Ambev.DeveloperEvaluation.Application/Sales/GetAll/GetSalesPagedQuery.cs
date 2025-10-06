using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetAll;

public record GetSalesPagedQuery(int Page = 1, int PageSize = 10) : IRequest<IEnumerable<GetSalesListItemResult>>;
