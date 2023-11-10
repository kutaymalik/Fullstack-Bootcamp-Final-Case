using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using OrderAutomationsProject.Data.Domain;

namespace OrderAutomationsProject.Data.Configuration;

public class AddressConfiguration : IEntityTypeConfiguration<Address>
{
    public void Configure(EntityTypeBuilder<Address> builder)
    {
        builder.Property(x => x.CreatedById).IsRequired();
        builder.Property(x => x.UpdatedById).IsRequired().HasDefaultValue(0);
        builder.Property(x => x.InsertDate).IsRequired();
        builder.Property(x => x.UpdateDate).IsRequired(false);
        builder.Property(x => x.IsActive).IsRequired().HasDefaultValue(true);

        builder.Property(x => x.DealerId).IsRequired();
        builder.Property(x => x.AddressLine1).IsRequired().HasMaxLength(50);
        builder.Property(x => x.AddressLine2).IsRequired().HasMaxLength(50);
        builder.Property(x => x.City).IsRequired().HasMaxLength(50);
        builder.Property(x => x.County).IsRequired().HasMaxLength(50);
        builder.Property(x => x.PostalCode).IsRequired().HasMaxLength(50);

        builder.HasIndex(x => x.DealerId);

        builder.HasOne(x => x.Dealer)
            .WithMany(x => x.Addresses)
            .HasForeignKey(x => x.DealerId);
    }
}
