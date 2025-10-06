using Ambev.DeveloperEvaluation.Application.Branches.CreateBranch;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Application.TestData;
using AutoMapper;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application;

public class CreateBranchHandlerTests
{
    private readonly IBranchRepository _branchRepository;
    private readonly IMapper _mapper;
    private readonly CreateBranchHandler _handler;

    public CreateBranchHandlerTests()
    {
        _branchRepository = Substitute.For<IBranchRepository>();
        _mapper = Substitute.For<IMapper>();
        _handler = new CreateBranchHandler(_branchRepository, _mapper);
    }

    [Fact(DisplayName = "Given valid branch data When creating branch Then returns success response")]
    public async Task Handle_ValidRequest_ReturnsSuccessResponse()
    {
        var command = CreateBranchHandlerTestData.GenerateValidCommand();
        var branch = CreateBranchHandlerTestData.GenerateValidBranch();
        var result = CreateBranchHandlerTestData.GenerateValidResult();

        _mapper.Map<Branch>(command).Returns(branch);
        _mapper.Map<CreateBranchResult>(branch).Returns(result);
        _branchRepository.CreateAsync(Arg.Any<Branch>(), Arg.Any<CancellationToken>())
            .Returns(branch);

        var createBranchResult = await _handler.Handle(command, CancellationToken.None);

        createBranchResult.Should().NotBeNull();
        createBranchResult.Id.Should().Be(result.Id);
        createBranchResult.Name.Should().Be(result.Name);
        createBranchResult.Code.Should().Be(result.Code);
        createBranchResult.Address.Should().Be(result.Address);
        createBranchResult.City.Should().Be(result.City);
        createBranchResult.State.Should().Be(result.State);
        createBranchResult.PostalCode.Should().Be(result.PostalCode);
        createBranchResult.Phone.Should().Be(result.Phone);
        createBranchResult.ManagerName.Should().Be(result.ManagerName);
        createBranchResult.Status.Should().Be(result.Status);
        await _branchRepository.Received(1).CreateAsync(Arg.Any<Branch>(), Arg.Any<CancellationToken>());
    }
}