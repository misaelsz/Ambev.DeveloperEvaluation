using Ambev.DeveloperEvaluation.Application.Branches.CreateBranch;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.WebApi.Features.Branches;
using Ambev.DeveloperEvaluation.WebApi.Features.Branches.CreateBranch;
using AutoMapper;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.WebApi.Branches;

public class BranchesControllerTests
{
    [Fact(DisplayName = "Given valid request When creating branch Then returns created response")]
    public async Task CreateBranch_ValidRequest_ReturnsCreated()
    {
        var mediator = Substitute.For<IMediator>();
        var mapper = Substitute.For<IMapper>();
        var controller = new BranchesController(mediator, mapper);

        var request = new CreateBranchRequest
        {
            Name = "Main Branch",
            Code = "MB-001",
            Address = "Av. Central, 100",
            City = "Sao Paulo",
            State = "SP",
            PostalCode = "01000-000",
            Phone = "(11) 1111-1111",
            ManagerName = "Manager",
            Status = Ambev.DeveloperEvaluation.Domain.Enums.BranchStatus.Active
        };

        var command = new CreateBranchCommand
        {
            Name = request.Name,
            Code = request.Code,
            Address = request.Address,
            City = request.City,
            State = request.State,
            PostalCode = request.PostalCode,
            Phone = request.Phone,
            ManagerName = request.ManagerName,
            Status = request.Status
        };

        var handlerResult = new CreateBranchResult
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Code = request.Code,
            Address = request.Address,
            City = request.City,
            State = request.State,
            PostalCode = request.PostalCode,
            Phone = request.Phone,
            ManagerName = request.ManagerName,
            Status = request.Status
        };

        var response = new CreateBranchResponse
        {
            Id = handlerResult.Id,
            Name = handlerResult.Name,
            Code = handlerResult.Code,
            Address = handlerResult.Address,
            City = handlerResult.City,
            State = handlerResult.State,
            PostalCode = handlerResult.PostalCode,
            Phone = handlerResult.Phone,
            ManagerName = handlerResult.ManagerName,
            Status = handlerResult.Status
        };

        mapper.Map<CreateBranchCommand>(request).Returns(command);
        mediator.Send(Arg.Is<CreateBranchCommand>(c => c.Name == command.Name), Arg.Any<CancellationToken>()).Returns(handlerResult);
        mapper.Map<CreateBranchResponse>(handlerResult).Returns(response);

        var result = await controller.CreateBranch(request, CancellationToken.None);

        result.Should().BeOfType<CreatedResult>();
        var created = (CreatedResult)result;
        created.Value.Should().BeOfType<ApiResponseWithData<CreateBranchResponse>>();
        var payload = (ApiResponseWithData<CreateBranchResponse>)created.Value!;
        payload.Success.Should().BeTrue();
        payload.Data.Should().NotBeNull();
        payload.Data!.Id.Should().Be(handlerResult.Id);
        payload.Data.Name.Should().Be(request.Name);
        await mediator.Received(1).Send(Arg.Any<CreateBranchCommand>(), Arg.Any<CancellationToken>());
    }
}
