using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

public class SaleItem : BaseEntity
{
    public Guid SaleId { get; set; }

    public Guid ProductId { get; set; }

    public string ProductName { get; set; } = string.Empty;

    public string ProductCode { get; set; } = string.Empty;

    public int Quantity { get; set; }

    public decimal UnitPrice { get; set; }

    public decimal DiscountPercentage { get; set; }

    public decimal DiscountAmount { get; set; }

    public decimal TotalAmount { get; set; }

    public bool IsCancelled { get; set; }

    public virtual Sale Sale { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;

    public SaleItem()
    {
        IsCancelled = false;
    }

    public void ApplyDiscountRules()
    {
        if (Quantity > 20)
            throw new InvalidOperationException("Cannot sell more than 20 items of the same product");

        if (Quantity < 4)
        {
            DiscountPercentage = 0;
            DiscountAmount = 0;
        }
        else if (Quantity >= 4 && Quantity < 10)
        {
            DiscountPercentage = 10;
        }
        else if (Quantity >= 10 && Quantity <= 20)
        {
            DiscountPercentage = 20;
        }

        CalculateAmounts();
    }

    public void CalculateAmounts()
    {
        var subtotal = Quantity * UnitPrice;
        DiscountAmount = subtotal * (DiscountPercentage / 100);
        TotalAmount = subtotal - DiscountAmount;
    }

    public void Cancel()
    {
        IsCancelled = true;
        TotalAmount = 0;
    }

    public bool IsValid()
    {
        return Quantity > 0
            && Quantity <= 20
            && UnitPrice > 0
            && !string.IsNullOrEmpty(ProductName)
            && !string.IsNullOrEmpty(ProductCode);
    }
}