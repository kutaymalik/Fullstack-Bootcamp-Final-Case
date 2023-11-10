using OrderAutomationsProject.Data.Domain;

namespace OrderAutomationsProject.Schema;

public class OrderRequest
{
    public List<OrderItemRequest> OrderItems { get; set; }
    public bool OpenAccountOrder { get; set; }
}

public class OrderResponse
{
    public int Id { get; set; }
    public string? OrderNumber { get; set; }
    public bool? Confirmation { get; set; }
    public decimal TotalAmount { get; set; }
    public bool OpenAccountOrder { get; set; }
    public bool? PaymentStatus { get; set; }
    public int DealerId { get; set; }
    //public virtual Dealer Dealer { get; set; }
    public int? BillId { get; set; }
    //public virtual Bill? Bill { get; set; }
    public int? PaymentId { get; set; }
    //public virtual Payment? Payment { get; set; }
    public List<OrderItem> OrderItems { get; set; }
}

