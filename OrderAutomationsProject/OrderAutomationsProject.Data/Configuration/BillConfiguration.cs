using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using OrderAutomationsProject.Data.Domain;

namespace OrderAutomationsProject.Data.Configuration;

public class BillConfiguration : IEntityTypeConfiguration<Bill>
{
    public void Configure(EntityTypeBuilder<Bill> builder)
    {
        builder.Property(x => x.CreatedById).IsRequired();
        builder.Property(x => x.UpdatedById).IsRequired().HasDefaultValue(0);
        builder.Property(x => x.InsertDate).IsRequired();
        builder.Property(x => x.UpdateDate).IsRequired(false);
        builder.Property(x => x.IsActive).IsRequired().HasDefaultValue(true);

        builder.Property(x => x.BillDate).IsRequired();
        builder.Property(x => x.DealerName).IsRequired().HasMaxLength(50);
        builder.Property(x => x.DealerAddress).IsRequired().HasMaxLength(100);
        builder.Property(x => x.DealerEmail).IsRequired().HasMaxLength(30);
        builder.Property(x => x.DealerPhone).IsRequired().HasMaxLength(12);
        builder.Property(x => x.OrderId).IsRequired();
        builder.Property(x => x.OrderId).IsRequired();
    }
}
