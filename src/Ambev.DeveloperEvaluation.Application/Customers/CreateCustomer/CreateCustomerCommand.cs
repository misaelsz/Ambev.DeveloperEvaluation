using Ambev.DeveloperEvaluation.Domain.Enums;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Customers.CreateCustomer;

public class CreateCustomerCommand : IRequest<CreateCustomerResult>
{
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Document { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public CustomerStatus Status { get; set; }
}