using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

public class Sale : BaseEntity
{
    public string SaleNumber { get; set; } = string.Empty;

    public DateTime SaleDate { get; set; }

    public Guid CustomerId { get; set; }

    public string CustomerName { get; set; } = string.Empty;

    public string CustomerDocument { get; set; } = string.Empty;

    public Guid BranchId { get; set; }

    public string BranchName { get; set; } = string.Empty;

    public decimal SubtotalAmount { get; set; }

    public decimal TotalDiscountAmount { get; set; }

    public decimal TotalAmount { get; set; }

    public SaleStatus Status { get; set; }

    public bool IsCancelled { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public DateTime? CancelledAt { get; set; }

    public virtual Customer Customer { get; set; } = null!;

    public virtual Branch Branch { get; set; } = null!;

    public virtual ICollection<SaleItem> Items { get; set; } = new List<SaleItem>();

    public Sale()
    {
        SaleDate = DateTime.UtcNow;
        CreatedAt = DateTime.UtcNow;
        Status = SaleStatus.Pending;
        IsCancelled = false;
    }

    public void AddItem(SaleItem item)
    {
        if (IsCancelled)
            throw new InvalidOperationException("Cannot add items to a cancelled sale");

        if (Status == SaleStatus.Cancelled)
            throw new InvalidOperationException("Cannot add items to a cancelled sale");

        item.ApplyDiscountRules();
        
        Items.Add(item);
        CalculateTotals();
        UpdatedAt = DateTime.UtcNow;
    }

    public void RemoveItem(Guid itemId)
    {
        if (IsCancelled)
            throw new InvalidOperationException("Cannot remove items from a cancelled sale");

        var item = Items.FirstOrDefault(i => i.Id == itemId);
        if (item != null)
        {
            Items.Remove(item);
            CalculateTotals();
            UpdatedAt = DateTime.UtcNow;
        }
    }

    public void CancelItem(Guid itemId)
    {
        if (IsCancelled)
            throw new InvalidOperationException("Cannot cancel items from a cancelled sale");

        var item = Items.FirstOrDefault(i => i.Id == itemId);
        if (item != null)
        {
            item.Cancel();
            CalculateTotals();
            UpdatedAt = DateTime.UtcNow;
        }
    }

    public void Confirm()
    {
        if (IsCancelled)
            throw new InvalidOperationException("Cannot confirm a cancelled sale");

        if (!Items.Any() || Items.All(i => i.IsCancelled))
            throw new InvalidOperationException("Cannot confirm a sale without valid items");

        Status = SaleStatus.Confirmed;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Cancel()
    {
        Status = SaleStatus.Cancelled;
        IsCancelled = true;
        CancelledAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;

        foreach (var item in Items)
        {
            item.Cancel();
        }

        CalculateTotals();
    }

    public void CalculateTotals()
    {
        var activeItems = Items.Where(i => !i.IsCancelled).ToList();
        
        SubtotalAmount = activeItems.Sum(i => i.Quantity * i.UnitPrice);
        TotalDiscountAmount = activeItems.Sum(i => i.DiscountAmount);
        TotalAmount = activeItems.Sum(i => i.TotalAmount);
    }

    public bool IsValid()
    {
        return !string.IsNullOrEmpty(SaleNumber)
            && CustomerId != Guid.Empty
            && BranchId != Guid.Empty
            && Items.Any()
            && Items.All(i => i.IsValid());
    }

    public int GetTotalQuantity()
    {
        return Items.Where(i => !i.IsCancelled).Sum(i => i.Quantity);
    }

    public int GetActiveItemsCount()
    {
        return Items.Count(i => !i.IsCancelled);
    }
}
