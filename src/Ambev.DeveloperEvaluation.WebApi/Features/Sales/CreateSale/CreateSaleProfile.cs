using AutoMapper;
using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;

public class CreateSaleProfile : Profile
{
    public CreateSaleProfile()
    {
        CreateMap<CreateSaleItemRequest, SaleItem>();
        CreateMap<CreateSaleRequest, CreateSaleCommand>()
            .ForMember(d => d.Itens, o => o.MapFrom(s => s.Items));
        CreateMap<CreateSaleResult, CreateSaleResponse>();
    }
}
