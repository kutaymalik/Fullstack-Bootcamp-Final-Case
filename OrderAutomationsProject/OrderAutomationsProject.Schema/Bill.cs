using OrderAutomationsProject.Data.Domain;

namespace OrderAutomationsProject.Schema;

public class BillRequest
{
    public DateTime BillDate { get; set; }
    public int OrderId { get; set; }
}

public class BillResponse
{
    public int Id {  get; set; }
    public DateTime BillDate { get; set; }
    public string DealerName { get; set; }
    public string DealerAddress { get; set; }
    public string DealerEmail { get; set; }
    public string DealerPhone { get; set; }
    public int OrderNumber { get; set; }
    public int OrderId { get; set; }
    public Order Order { get; set; }
    public List<OrderItem> OrderItems { get; set; }
    public decimal TotalAmount { get; set; }
}