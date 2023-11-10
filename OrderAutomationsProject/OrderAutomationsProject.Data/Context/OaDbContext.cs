using Microsoft.EntityFrameworkCore;
using OrderAutomationsProject.Data.Configuration;
using OrderAutomationsProject.Data.Domain;

namespace OrderAutomationsProject.Data.Context;

public class OaDbContext : DbContext
{
    public OaDbContext(DbContextOptions<OaDbContext> options) : base(options)
    {

    }

    public DbSet<Dealer> Dealers { get; set; }
    public DbSet<Address> Addresses { get; set; }
    public DbSet<Bill> Bills { get; set; }
    public DbSet<Card> Cards { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Cost> Costs { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Payment> Payments { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            // Veritabanı bağlantınızı burada yapılandırın...

            // Duyarlı veri günlüğünü etkinleştirme
            optionsBuilder.EnableSensitiveDataLogging();
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new AddressConfiguration());
        modelBuilder.ApplyConfiguration(new BillConfiguration());
        modelBuilder.ApplyConfiguration(new CardConfiguration());
        modelBuilder.ApplyConfiguration(new CategoryConfiguration());
        modelBuilder.ApplyConfiguration(new CostConfiguration());
        modelBuilder.ApplyConfiguration(new DealerConfiguration());
        modelBuilder.ApplyConfiguration(new OrderConfiguration());
        modelBuilder.ApplyConfiguration(new PaymentConfiguration());
        modelBuilder.ApplyConfiguration(new ProductConfiguration());
        modelBuilder.ApplyConfiguration(new OrderItemConfiguration());
        modelBuilder.ApplyConfiguration(new MessageConfiguration());

        base.OnModelCreating(modelBuilder);
    }


}
