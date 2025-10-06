using AutoMapper;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Application.Customers.CreateCustomer;

public class CreateCustomerProfile : Profile
{
    public CreateCustomerProfile()
    {
        CreateMap<CreateCustomerCommand, Customer>();
        CreateMap<Customer, CreateCustomerResult>();
    }
}