using OrderAutomationsProject.Data.Domain;

namespace OrderAutomationsProject.Schema;

public class OrderItemRequest
{
    public int ProductId { get; set; }
    public int Quantity { get; set; }
}

public class OrderItemResponse
{
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal TotalPrice { get; set; }
    public int OrderId { get; set; }
    public int ProductId { get; set; }
    public virtual Product Product { get; set; }
}

