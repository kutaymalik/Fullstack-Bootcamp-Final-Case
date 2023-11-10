using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using OrderAutomationsProject.Data.Domain;

namespace OrderAutomationsProject.Data.Configuration;

public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
{
    public void Configure(EntityTypeBuilder<Payment> builder)
    {
        // BaseModel configuration
        builder.Property(x => x.CreatedById).IsRequired();
        builder.Property(x => x.UpdatedById).IsRequired().HasDefaultValue(0);
        builder.Property(x => x.InsertDate).IsRequired();
        builder.Property(x => x.UpdateDate).IsRequired(false);
        builder.Property(x => x.IsActive).IsRequired().HasDefaultValue(true);

        builder.Property(p => p.PaymentAmount).HasPrecision(18, 2).IsRequired();
        builder.Property(p => p.PaymentDate).IsRequired();
        builder.Property(p => p.PaymentDescription).HasMaxLength(100);

        builder.Property(p => p.PaymentType)
                .HasConversion<string>();

        // Order ilişkisi
        builder.HasOne(p => p.Order)
            .WithOne(o => o.Payment)
            .HasForeignKey<Payment>(p => p.OrderId)
            .IsRequired();

        builder.HasOne(p => p.Card)
            .WithOne()
            .HasForeignKey<Payment>(p => p.CardId);
    }
}
