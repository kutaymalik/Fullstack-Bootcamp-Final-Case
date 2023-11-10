using OrderAutomationsProject.Data.Domain;
using OrderAutomationsProject.Data.Repository;

namespace OrderAutomationsProject.Data.UnitOfWorks;

public interface IUnitOfWork
{
    Task CompleteAsync(CancellationToken cancellationToken);
    void CompleteTransactionAsync(CancellationToken cancellationToken);
    IGenericRepository<Dealer> DealerRepository { get; }
    IGenericRepository<Address> AddressRepository { get; }
    IGenericRepository<Bill> BillRepository { get; }
    IGenericRepository<Card> CardRepository { get; }
    IGenericRepository<Category> CategoryRepository { get; }
    IGenericRepository<Order> OrderRepository { get; }
    IGenericRepository<OrderItem> OrderItemRepository { get; }
    IGenericRepository<Payment> PaymentRepository { get; }
    IGenericRepository<Product> ProductRepository { get; }
    IGenericRepository<Cost> CostRepository { get; }
    IGenericRepository<Message> MessageRepository { get; }
}

