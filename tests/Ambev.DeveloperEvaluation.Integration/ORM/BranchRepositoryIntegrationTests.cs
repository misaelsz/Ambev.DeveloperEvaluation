using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Integration.TestData;
using Ambev.DeveloperEvaluation.ORM;
using Ambev.DeveloperEvaluation.ORM.Repositories;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Ambev.DeveloperEvaluation.Integration.ORM;

public class BranchRepositoryIntegrationTests : IDisposable
{
    private readonly DefaultContext _context;
    private readonly IBranchRepository _branchRepository;

    public BranchRepositoryIntegrationTests()
    {
        var options = new DbContextOptionsBuilder<DefaultContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new DefaultContext(options);
        _branchRepository = new BranchRepository(_context);
    }

    [Fact(DisplayName = "Given valid branch When creating Then persists and returns created branch")]
    public async Task CreateAsync_ValidBranch_PersistsAndReturnsCreatedBranch()
    {
        var branch = BranchRepositoryIntegrationTestData.GenerateValidBranch();

        var result = await _branchRepository.CreateAsync(branch, CancellationToken.None);

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

        var persistedBranch = await _context.Branches.FindAsync(result.Id);
        persistedBranch.Should().NotBeNull();
        persistedBranch!.Name.Should().Be(branch.Name);
        persistedBranch.Code.Should().Be(branch.Code);
        persistedBranch.Address.Should().Be(branch.Address);
        persistedBranch.City.Should().Be(branch.City);
        persistedBranch.State.Should().Be(branch.State);
        persistedBranch.PostalCode.Should().Be(branch.PostalCode);
        persistedBranch.Phone.Should().Be(branch.Phone);
        persistedBranch.ManagerName.Should().Be(branch.ManagerName);
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}