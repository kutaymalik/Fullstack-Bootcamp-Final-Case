using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderAutomationsProject.Data.Domain;

namespace OrderAutomationsProject.Data.Configuration;

public class CostConfiguration : IEntityTypeConfiguration<Cost>
{
    public void Configure(EntityTypeBuilder<Cost> builder)
    {
        builder.Property(x => x.CreatedById).IsRequired();
        builder.Property(x => x.UpdatedById).IsRequired().HasDefaultValue(0);
        builder.Property(x => x.InsertDate).IsRequired();
        builder.Property(x => x.UpdateDate).IsRequired(false);
        builder.Property(x => x.IsActive).IsRequired().HasDefaultValue(true);

        builder.Property(x => x.CostDescription).HasMaxLength(100).IsRequired();

        builder.Property(x => x.CostAmount).HasPrecision(18, 2).IsRequired();
        builder.Property(x => x.Confirmation);

        builder.HasIndex(x => x.DealerId);

        // Dealer Configuration
        builder.HasOne(c => c.Dealer)
            .WithMany(d => d.Costs)
            .HasForeignKey(c => c.DealerId);
    }
}
