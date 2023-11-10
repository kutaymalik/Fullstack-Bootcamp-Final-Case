using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using OrderAutomationsProject.Data.Domain;

namespace OrderAutomationsProject.Data.Configuration;

public class DealerConfiguration : IEntityTypeConfiguration<Dealer>
{
    public void Configure(EntityTypeBuilder<Dealer> builder)
    {
        // BaseModel configuration
        builder.Property(x => x.CreatedById).IsRequired();
        builder.Property(x => x.UpdatedById).IsRequired().HasDefaultValue(0);
        builder.Property(x => x.InsertDate).IsRequired();
        builder.Property(x => x.UpdateDate).IsRequired(false);
        builder.Property(x => x.IsActive).IsRequired().HasDefaultValue(true);

        // Dealer Model Configuration
        builder.Property(a => a.DealerNumber).IsRequired();
        builder.Property(a => a.FirstName).HasMaxLength(50).IsRequired();
        builder.Property(a => a.LastName).HasMaxLength(50).IsRequired();
        builder.Property(a => a.Email).HasMaxLength(50).IsRequired();
        builder.Property(a => a.PasswordHash).IsRequired();
        builder.Property(a => a.Role).HasMaxLength(10);
        builder.Property(d => d.PhoneNumber).HasMaxLength(20);
        builder.Property(d => d.Dividend).HasPrecision(18, 2);
        builder.Property(d => d.OpenAccountLimit).HasPrecision(18, 2).IsRequired();
    }
}
