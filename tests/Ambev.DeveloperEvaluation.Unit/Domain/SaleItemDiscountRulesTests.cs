using Ambev.DeveloperEvaluation.Domain.Entities;
using FluentAssertions;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain;

public class SaleItemDiscountRulesTests
{
    [Theory(DisplayName = "Given quantity between 4 and 9 When applying discount Then applies 10 percent")]
    [InlineData(4)]
    [InlineData(5)]
    [InlineData(9)]
    public void ApplyDiscountRules_Quantity4To9_Applies10Percent(int quantity)
    {
        var item = new SaleItem
        {
            ProductId = Guid.NewGuid(),
            ProductName = "Product",
            ProductCode = "CODE",
            Quantity = quantity,
            UnitPrice = 100m
        };

        item.ApplyDiscountRules();

        item.DiscountPercentage.Should().Be(10m);
        item.DiscountAmount.Should().Be(quantity * 100m * 0.10m);
        item.TotalAmount.Should().Be(quantity * 100m - item.DiscountAmount);
    }

    [Theory(DisplayName = "Given quantity between 10 and 20 When applying discount Then applies 20 percent")]
    [InlineData(10)]
    [InlineData(15)]
    [InlineData(20)]
    public void ApplyDiscountRules_Quantity10To20_Applies20Percent(int quantity)
    {
        var item = new SaleItem
        {
            ProductId = Guid.NewGuid(),
            ProductName = "Product",
            ProductCode = "CODE",
            Quantity = quantity,
            UnitPrice = 100m
        };

        item.ApplyDiscountRules();

        item.DiscountPercentage.Should().Be(20m);
        item.DiscountAmount.Should().Be(quantity * 100m * 0.20m);
        item.TotalAmount.Should().Be(quantity * 100m - item.DiscountAmount);
    }

    [Theory(DisplayName = "Given quantity below 4 When applying discount Then no discount is applied")]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    public void ApplyDiscountRules_QuantityBelow4_NoDiscount(int quantity)
    {
        var item = new SaleItem
        {
            ProductId = Guid.NewGuid(),
            ProductName = "Product",
            ProductCode = "CODE",
            Quantity = quantity,
            UnitPrice = 100m
        };

        item.ApplyDiscountRules();

        item.DiscountPercentage.Should().Be(0m);
        item.DiscountAmount.Should().Be(0m);
        item.TotalAmount.Should().Be(quantity * 100m);
    }

    [Fact(DisplayName = "Given quantity above 20 When applying discount Then throws invalid operation")]
    public void ApplyDiscountRules_QuantityAbove20_Throws()
    {
        var item = new SaleItem
        {
            ProductId = Guid.NewGuid(),
            ProductName = "Product",
            ProductCode = "CODE",
            Quantity = 21,
            UnitPrice = 100m
        };

        var act = () => item.ApplyDiscountRules();

        act.Should().Throw<InvalidOperationException>()
            .WithMessage("Cannot sell more than 20 items of the same product");
    }
}
