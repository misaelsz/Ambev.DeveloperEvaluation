using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetById;

public record GetSaleByIdCommand(Guid Id) : IRequest<GetSaleByIdResult>;
