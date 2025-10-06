using MediatR;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Application.Sales.GetById;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetById;
using Ambev.DeveloperEvaluation.Application.Sales.GetAll;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetAll;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales;

[ApiController]
[Route("api/[controller]")]
public class SalesController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public SalesController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpPost]
    [ProducesResponseType(typeof(ApiResponseWithData<CreateSaleResponse>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateSale([FromBody] CreateSaleRequest request, CancellationToken cancellationToken)
    {
        var validator = new CreateSaleValidator();
        var command = _mapper.Map<CreateSaleCommand>(request);
        var validationResult = await validator.ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        var result = await _mediator.Send(command, cancellationToken);

        return Created(string.Empty, new ApiResponseWithData<CreateSaleResponse>
        {
            Success = true,
            Message = "Sale created successfully",
            Data = _mapper.Map<CreateSaleResponse>(result)
        });
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ApiResponseWithData<GetSaleByIdResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetSaleByIdCommand(id), cancellationToken);
        if (result is null)
            return NotFound(new ApiResponse { Success = false, Message = "Sale not found" });

        return Ok(new ApiResponseWithData<GetSaleByIdResponse>
        {
            Success = true,
            Message = "Sale retrieved successfully",
            Data = _mapper.Map<GetSaleByIdResponse>(result)
        });
    }

    [HttpGet]
    [ProducesResponseType(typeof(PaginatedResponse<GetSalesListItemResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 10, CancellationToken cancellationToken = default)
    {
        var results = await _mediator.Send(new GetSalesPagedQuery(page, pageSize), cancellationToken);
        var data = _mapper.Map<IEnumerable<GetSalesListItemResponse>>(results);

        return Ok(new PaginatedResponse<GetSalesListItemResponse>
        {
            Success = true,
            Message = "Sales retrieved successfully",
            Data = data,
            CurrentPage = page,
            TotalPages = (int)Math.Ceiling((double)data.Count() / pageSize),
            TotalCount = data.Count()
        });
    }
}
