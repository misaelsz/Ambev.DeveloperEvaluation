using AutoMapper;
using Ambev.DeveloperEvaluation.Application.Sales.GetById;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetById;

public class GetSaleByIdProfile : Profile
{
    public GetSaleByIdProfile()
    {
        CreateMap<GetSaleByIdResult, GetSaleByIdResponse>();
    }
}
