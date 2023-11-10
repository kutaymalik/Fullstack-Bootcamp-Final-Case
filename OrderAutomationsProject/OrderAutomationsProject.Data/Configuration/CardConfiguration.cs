using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using OrderAutomationsProject.Data.Domain;

namespace OrderAutomationsProject.Data.Configuration;

public class CardConfiguration : IEntityTypeConfiguration<Card>
{
    public void Configure(EntityTypeBuilder<Card> builder)
    {
        builder.Property(x => x.CreatedById).IsRequired();
        builder.Property(x => x.UpdatedById).IsRequired().HasDefaultValue(0);
        builder.Property(x => x.InsertDate).IsRequired();
        builder.Property(x => x.UpdateDate).IsRequired(false);
        builder.Property(x => x.IsActive).IsRequired().HasDefaultValue(true);

        builder.Property(c => c.BankName)
            .HasConversion<string>();

        builder.Property(c => c.CardHolderType)
            .HasConversion<string>();

        builder.Property(c => c.CardNumber).HasMaxLength(14).IsRequired();
        builder.Property(c => c.ExpiryDate).IsRequired();
        builder.Property(c => c.CVV).HasMaxLength(3).IsRequired();
        builder.Property(c => c.ExpenseLimit).HasColumnType("decimal(18,2)").HasPrecision(18, 2).IsRequired();

        builder.HasIndex(x => x.DealerId);

        builder.HasOne(c => c.Dealer)
            .WithMany(d => d.Cards)
            .HasForeignKey(c => c.DealerId);
    }
}