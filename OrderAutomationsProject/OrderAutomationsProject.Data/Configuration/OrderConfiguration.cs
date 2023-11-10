using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using OrderAutomationsProject.Data.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Text.RegularExpressions;

namespace OrderAutomationsProject.Data.Configuration;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        // BaseModel configuration
        builder.Property(x => x.CreatedById).IsRequired();
        builder.Property(x => x.UpdatedById).IsRequired().HasDefaultValue(0);
        builder.Property(x => x.InsertDate).IsRequired();
        builder.Property(x => x.UpdateDate).IsRequired(false);
        builder.Property(x => x.IsActive).IsRequired().HasDefaultValue(true);

        builder.Property(o => o.OrderNumber).HasMaxLength(6);
        builder.Property(o => o.Confirmation);
        builder.Property(o => o.TotalAmount).HasColumnType("decimal(18,2)").HasPrecision(18, 2);

        // Dealer relationship
        builder.HasOne(o => o.Dealer)
            .WithMany(d => d.Orders)
            .HasForeignKey(o => o.DealerId)
            .IsRequired();

        // Bill relationship
        builder.HasOne(o => o.Bill)
            .WithOne(b => b.Order)
            .HasForeignKey<Order>(o => o.BillId);

        // OrderItem relationship
        builder.HasMany(o => o.OrderItems)
            .WithOne()
            .HasForeignKey("OrderId");
    }
}

public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        // BaseModel configuration
        builder.Property(x => x.CreatedById).IsRequired();
        builder.Property(x => x.UpdatedById).IsRequired().HasDefaultValue(0);
        builder.Property(x => x.InsertDate).IsRequired();
        builder.Property(x => x.UpdateDate).IsRequired(false);
        builder.Property(x => x.IsActive).IsRequired().HasDefaultValue(true);

        builder.Property(oi => oi.Quantity).IsRequired();
        builder.Property(oi => oi.UnitPrice).HasPrecision(18, 2).IsRequired();
        builder.Property(oi => oi.TotalPrice).HasPrecision(18,2).IsRequired();

        // Product relationship(matches "ProductId" column by default)
        builder.HasOne(oi => oi.Product)
            .WithMany()
            .HasForeignKey(oi => oi.ProductId);
    }
}
