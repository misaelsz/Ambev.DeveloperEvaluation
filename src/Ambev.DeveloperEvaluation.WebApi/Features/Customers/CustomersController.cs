using MediatR;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.WebApi.Features.Customers.CreateCustomer;
using Ambev.DeveloperEvaluation.Application.Customers.CreateCustomer;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Customers;

[ApiController]
[Route("api/[controller]")]
public class CustomersController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public CustomersController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpPost]
    [ProducesResponseType(typeof(ApiResponseWithData<CreateCustomerResponse>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateCustomer([FromBody] CreateCustomerRequest request, CancellationToken cancellationToken)
    {
        var validator = new CreateCustomerValidator();
        var command = _mapper.Map<CreateCustomerCommand>(request);
        var validationResult = await validator.ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        var result = await _mediator.Send(command, cancellationToken);

        return Created(string.Empty, new ApiResponseWithData<CreateCustomerResponse>
        {
            Success = true,
            Message = "Customer created successfully",
            Data = _mapper.Map<CreateCustomerResponse>(result)
        });
    }
}
