using Ambev.DeveloperEvaluation.Domain.Enums;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.CreateProduct;

public class CreateProductCommand : IRequest<CreateProductResult>
{
    public string Name { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public decimal UnitPrice { get; set; }
    public ProductStatus Status { get; set; }
}