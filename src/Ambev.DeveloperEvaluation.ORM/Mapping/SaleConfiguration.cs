using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Mapping;

public class SaleConfiguration : IEntityTypeConfiguration<Sale>
{
    public void Configure(EntityTypeBuilder<Sale> builder)
    {
        builder.ToTable("Sales");

        builder.HasKey(s => s.Id);
        
        builder.Property(s => s.Id)
            .HasColumnType("uuid")
            .HasDefaultValueSql("gen_random_uuid()");

        builder.Property(s => s.SaleNumber)
            .IsRequired()
            .HasMaxLength(50)
            .HasComment("Unique business identifier for the sale");

        builder.Property(s => s.SaleDate)
            .IsRequired()
            .HasComment("Date and time when the sale was made");

        builder.Property(s => s.CustomerId)
            .IsRequired()
            .HasComment("Foreign key reference to Customer");

        builder.Property(s => s.CustomerName)
            .IsRequired()
            .HasMaxLength(200)
            .HasComment("Customer name denormalized for performance");

        builder.Property(s => s.CustomerDocument)
            .IsRequired()
            .HasMaxLength(20)
            .HasComment("Customer document denormalized for performance");

        builder.Property(s => s.BranchId)
            .IsRequired()
            .HasComment("Foreign key reference to Branch");

        builder.Property(s => s.BranchName)
            .IsRequired()
            .HasMaxLength(200)
            .HasComment("Branch name denormalized for performance");

        builder.Property(s => s.SubtotalAmount)
            .HasColumnType("decimal(18,2)")
            .IsRequired()
            .HasDefaultValue(0)
            .HasComment("Total amount before discounts");

        builder.Property(s => s.TotalDiscountAmount)
            .HasColumnType("decimal(18,2)")
            .IsRequired()
            .HasDefaultValue(0)
            .HasComment("Total discount amount applied");

        builder.Property(s => s.TotalAmount)
            .HasColumnType("decimal(18,2)")
            .IsRequired()
            .HasDefaultValue(0)
            .HasComment("Final total amount after discounts");

        builder.Property(s => s.Status)
            .HasConversion<string>()
            .HasMaxLength(20)
            .IsRequired()
            .HasComment("Current status of the sale");

        builder.Property(s => s.IsCancelled)
            .IsRequired()
            .HasDefaultValue(false)
            .HasComment("Indicates if the sale is cancelled");

        builder.Property(s => s.CreatedAt)
            .IsRequired()
            .HasComment("Date and time when the sale was created in the system");

        builder.Property(s => s.UpdatedAt)
            .HasComment("Date and time of the last update");

        builder.Property(s => s.CancelledAt)
            .HasComment("Date and time when the sale was cancelled");


        builder.HasOne(s => s.Customer)
            .WithMany(c => c.Sales)
            .HasForeignKey(s => s.CustomerId)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("FK_Sales_Customer");

        builder.HasOne(s => s.Branch)
            .WithMany(b => b.Sales)
            .HasForeignKey(s => s.BranchId)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("FK_Sales_Branch");

        builder.HasMany(s => s.Items)
            .WithOne(si => si.Sale)
            .HasForeignKey(si => si.SaleId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("FK_SaleItems_Sale");

    }
}