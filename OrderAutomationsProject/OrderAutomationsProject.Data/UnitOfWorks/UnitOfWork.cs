using OrderAutomationsProject.Data.Context;
using OrderAutomationsProject.Data.Domain;
using OrderAutomationsProject.Data.Repository;
using Serilog;

namespace OrderAutomationsProject.Data.UnitOfWorks;

public class UnitOfWork : IUnitOfWork
{
    private readonly OaDbContext dbContext;

    public UnitOfWork(OaDbContext dbContext)
    {
        this.dbContext = dbContext;
        DealerRepository = new GenericRepository<Dealer>(dbContext);
        AddressRepository = new GenericRepository<Address>(dbContext);
        BillRepository = new GenericRepository<Bill>(dbContext);
        CardRepository = new GenericRepository<Card>(dbContext);
        CategoryRepository = new GenericRepository<Category>(dbContext);
        OrderRepository = new GenericRepository<Order>(dbContext);
        OrderItemRepository = new GenericRepository<OrderItem>(dbContext);
        PaymentRepository = new GenericRepository<Payment>(dbContext);
        ProductRepository = new GenericRepository<Product>(dbContext);
        CostRepository = new GenericRepository<Cost>(dbContext);
        MessageRepository = new GenericRepository<Message>(dbContext);
    }

    public IGenericRepository<Dealer> DealerRepository { get; private set; }

    public IGenericRepository<Address> AddressRepository { get; private set; }

    public IGenericRepository<Bill> BillRepository { get; private set; }

    public IGenericRepository<Card> CardRepository { get; private set; }

    public IGenericRepository<Category> CategoryRepository { get; private set; }

    public IGenericRepository<Order> OrderRepository { get; private set; }

    public IGenericRepository<OrderItem> OrderItemRepository { get; private set; }

    public IGenericRepository<Payment> PaymentRepository { get; private set; }

    public IGenericRepository<Product> ProductRepository { get; private set; }

    public IGenericRepository<Cost> CostRepository { get; private set; }
    public IGenericRepository<Message> MessageRepository { get; private set; }

    public async Task CompleteAsync(CancellationToken cancellationToken)
    {
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async void CompleteTransactionAsync(CancellationToken cancellationToken)
    {
        using (var transaction = dbContext.Database.BeginTransaction())
        {
            try
            {
                await dbContext.SaveChangesAsync(cancellationToken);
                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                Log.Error("CompleteTransaction", ex);
            }
        }
    }
}

