using AutoMapper;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetAll;

public class GetSalesPagedProfile : Profile
{
    public GetSalesPagedProfile()
    {
        CreateMap<Sale, GetSalesListItemResult>();
    }
}
