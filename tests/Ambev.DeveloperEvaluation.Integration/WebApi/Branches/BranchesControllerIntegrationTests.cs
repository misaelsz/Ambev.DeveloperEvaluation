using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.WebApi.Features.Branches;
using Ambev.DeveloperEvaluation.WebApi.Features.Branches.CreateBranch;
using AutoMapper;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Ambev.DeveloperEvaluation.Integration.WebApi.Branches;

public class BranchesControllerIntegrationTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly CustomWebApplicationFactory _factory;

    public BranchesControllerIntegrationTests(CustomWebApplicationFactory factory)
    {
        _factory = factory;
    }

    [Fact(DisplayName = "Given valid request When creating branch via controller Then returns created response")]
    public async Task CreateBranch_ValidRequest_PersistsAndReturnsCreated()
    {
        using var scope = _factory.Services.CreateScope();
        var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
        var mapper = scope.ServiceProvider.GetRequiredService<IMapper>();
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

        var result = await controller.CreateBranch(request, CancellationToken.None);

        result.Should().BeOfType<CreatedResult>();
        var created = (CreatedResult)result;
        created.Value.Should().BeOfType<ApiResponseWithData<CreateBranchResponse>>();
        var payload = (ApiResponseWithData<CreateBranchResponse>)created.Value!;
        payload.Success.Should().BeTrue();
        payload.Data.Should().NotBeNull();
        payload.Data!.Name.Should().Be(request.Name);
        payload.Data.Code.Should().Be(request.Code);
    }
}
