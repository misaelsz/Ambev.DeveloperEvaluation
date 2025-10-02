using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

public class Customer : BaseEntity
{
    public string Name { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string Phone { get; set; } = string.Empty;

    public string Document { get; set; } = string.Empty;

    public string Address { get; set; } = string.Empty;

    public CustomerStatus Status { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<Sale> Sales { get; set; } = new List<Sale>();

    public Customer()
    {
        CreatedAt = DateTime.UtcNow;
        Status = CustomerStatus.Active;
    }

    public void Activate()
    {
        Status = CustomerStatus.Active;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Deactivate()
    {
        Status = CustomerStatus.Inactive;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Block()
    {
        Status = CustomerStatus.Blocked;
        UpdatedAt = DateTime.UtcNow;
    }
}