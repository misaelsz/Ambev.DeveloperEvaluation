using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Repositories;

public interface IBranchRepository
{
    Task<Branch> CreateAsync(Branch branch, CancellationToken cancellationToken = default);

    Task<Branch?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task<Branch?> GetByCodeAsync(string code, CancellationToken cancellationToken = default);

    Task<Branch> UpdateAsync(Branch branch, CancellationToken cancellationToken = default);

    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);

    Task<IEnumerable<Branch>> GetAllAsync(int page = 1, int pageSize = 10, CancellationToken cancellationToken = default);

    Task<IEnumerable<Branch>> GetActiveAsync(CancellationToken cancellationToken = default);
}