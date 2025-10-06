using MediatR;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.WebApi.Features.Branches.CreateBranch;
using Ambev.DeveloperEvaluation.Application.Branches.CreateBranch;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Branches;

[ApiController]
[Route("api/[controller]")]
public class BranchesController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public BranchesController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpPost]
    [ProducesResponseType(typeof(ApiResponseWithData<CreateBranchResponse>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateBranch([FromBody] CreateBranchRequest request, CancellationToken cancellationToken)
    {
        var validator = new CreateBranchValidator();
        var command = _mapper.Map<CreateBranchCommand>(request);
        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        var result = await _mediator.Send(command, cancellationToken);

        return Created(string.Empty, new ApiResponseWithData<CreateBranchResponse>
        {
            Success = true,
            Message = "Branch created successfully",
            Data = _mapper.Map<CreateBranchResponse>(result)
        });
    }
}
