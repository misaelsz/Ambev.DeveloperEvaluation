using AutoMapper;
using Ambev.DeveloperEvaluation.Application.Sales.GetAll;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetAll;

public class GetSalesPagedProfile : Profile
{
    public GetSalesPagedProfile()
    {
        CreateMap<GetSalesListItemResult, GetSalesListItemResponse>();
    }
}
