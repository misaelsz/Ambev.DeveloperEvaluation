using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.ORM.TestData;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.ORM;

public class BranchRepositoryTests
{
    [Fact(DisplayName = "Given valid branch When creating Then returns created branch")]
    public async Task CreateAsync_ValidBranch_ReturnsCreatedBranch()
    {
        var branchRepository = Substitute.For<IBranchRepository>();
        var branch = BranchRepositoryTestData.GenerateValidBranch();

        branchRepository.CreateAsync(Arg.Any<Branch>(), Arg.Any<CancellationToken>())
            .Returns(branch);

        var result = await branchRepository.CreateAsync(branch, CancellationToken.None);

        result.Should().NotBeNull();
        result.Id.Should().Be(branch.Id);
        result.Name.Should().Be(branch.Name);
        result.Code.Should().Be(branch.Code);
        result.Address.Should().Be(branch.Address);
        result.City.Should().Be(branch.City);
        result.State.Should().Be(branch.State);
        result.PostalCode.Should().Be(branch.PostalCode);
        result.Phone.Should().Be(branch.Phone);
        result.ManagerName.Should().Be(branch.ManagerName);
        result.Status.Should().Be(branch.Status);
        await branchRepository.Received(1).CreateAsync(Arg.Any<Branch>(), Arg.Any<CancellationToken>());
    }
}