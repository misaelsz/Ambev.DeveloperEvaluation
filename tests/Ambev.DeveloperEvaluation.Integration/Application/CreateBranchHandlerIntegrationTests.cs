using Ambev.DeveloperEvaluation.Application.Branches.CreateBranch;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Integration.TestData;
using Ambev.DeveloperEvaluation.ORM;
using Ambev.DeveloperEvaluation.ORM.Repositories;
using AutoMapper;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Ambev.DeveloperEvaluation.Integration.Application;

public class CreateBranchHandlerIntegrationTests : IDisposable
{
    private readonly DefaultContext _context;
    private readonly IBranchRepository _branchRepository;
    private readonly IMapper _mapper;
    private readonly CreateBranchHandler _handler;

    public CreateBranchHandlerIntegrationTests()
    {
        var options = new DbContextOptionsBuilder<DefaultContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new DefaultContext(options);
        _branchRepository = new BranchRepository(_context);

        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<CreateBranchProfile>();
        });
        _mapper = config.CreateMapper();

        _handler = new CreateBranchHandler(_branchRepository, _mapper);
    }

    [Fact(DisplayName = "Given valid command When handling Then persists and returns created branch")]
    public async Task Handle_ValidCommand_PersistsAndReturnsCreatedBranch()
    {
        var command = CreateBranchHandlerIntegrationTestData.GenerateValidCommand();

        var result = await _handler.Handle(command, CancellationToken.None);

        result.Should().NotBeNull();
        result.Id.Should().NotBeEmpty();
        result.Name.Should().Be(command.Name);
        result.Code.Should().Be(command.Code);
        result.Address.Should().Be(command.Address);
        result.City.Should().Be(command.City);
        result.State.Should().Be(command.State);
        result.PostalCode.Should().Be(command.PostalCode);
        result.Phone.Should().Be(command.Phone);
        result.ManagerName.Should().Be(command.ManagerName);
        result.Status.Should().Be(command.Status);

        var persistedBranch = await _context.Branches.FindAsync(result.Id);
        persistedBranch.Should().NotBeNull();
        persistedBranch!.Name.Should().Be(command.Name);
        persistedBranch.Code.Should().Be(command.Code);
        persistedBranch.Address.Should().Be(command.Address);
        persistedBranch.City.Should().Be(command.City);
        persistedBranch.State.Should().Be(command.State);
        persistedBranch.PostalCode.Should().Be(command.PostalCode);
        persistedBranch.Phone.Should().Be(command.Phone);
        persistedBranch.ManagerName.Should().Be(command.ManagerName);
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}