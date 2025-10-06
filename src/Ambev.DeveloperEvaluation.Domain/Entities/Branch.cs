using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

public class Branch : BaseEntity
{
    public string Name { get; set; } = string.Empty;

    public string Code { get; set; } = string.Empty;

    public string Address { get; set; } = string.Empty;

    public string City { get; set; } = string.Empty;

    public string State { get; set; } = string.Empty;

    public string PostalCode { get; set; } = string.Empty;

    public string Phone { get; set; } = string.Empty;

    public string ManagerName { get; set; } = string.Empty;

    public BranchStatus Status { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<Sale> Sales { get; set; } = new List<Sale>();

    public Branch()
    {
        CreatedAt = DateTime.UtcNow;
        Status = BranchStatus.Active;
    }

    public void Activate()
    {
        Status = BranchStatus.Active;
        UpdatedAt = DateTime.UtcNow;
    }


    public void Deactivate()
    {
        Status = BranchStatus.Inactive;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Close()
    {
        Status = BranchStatus.Closed;
        UpdatedAt = DateTime.UtcNow;
    }
}