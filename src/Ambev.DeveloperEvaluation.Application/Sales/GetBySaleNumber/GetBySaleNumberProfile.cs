using AutoMapper;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetBySaleNumber;

public class GetBySaleNumberProfile : Profile
{
    public GetBySaleNumberProfile()
    {
        CreateMap<Sale, GetBySaleNumberResult>();
    }
}