using Ambev.DeveloperEvaluation.Application.Sales.GetById;
using Ambev.DeveloperEvaluation.WebApi.Common;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetById;

public class GetSaleByIdResponse
{
    public Guid Id { get; set; }
    public string SaleNumber { get; set; } = string.Empty;
    public DateTime SaleDate { get; set; }
    public Guid CustomerId { get; set; }
    public string CustomerName { get; set; } = string.Empty;
    public string CustomerDocument { get; set; } = string.Empty;
    public Guid BranchId { get; set; }
    public string BranchName { get; set; } = string.Empty;
    public decimal SubtotalAmount { get; set; }
    public decimal TotalDiscountAmount { get; set; }
    public decimal TotalAmount { get; set; }
    public Domain.Enums.SaleStatus Status { get; set; }
}
