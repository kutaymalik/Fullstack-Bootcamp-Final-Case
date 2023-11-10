using OrderAutomationsProject.Base.Model;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrderAutomationsProject.Data.Domain;

[Table("Order", Schema = "dbo")]
public class Order : BaseModel
{
    public string? OrderNumber { get; set; }
    public bool? Confirmation {  get; set; }
    public decimal TotalAmount { get; set; }
    public bool OpenAccountOrder { get; set; }
    public int DealerId { get; set; }
    public virtual Dealer Dealer { get; set; }
    public bool? PaymentStatus { get; set; } = false;
    public int? BillId { get; set; }
    public virtual Bill? Bill { get; set; }
    public int? PaymentId { get; set; }
    public virtual Payment? Payment { get; set; }
    public List<OrderItem>? OrderItems { get; set; }
    public int AllQuantity { get; set; }
}

public class OrderItem : BaseModel
{
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal TotalPrice { get; set; }
    public int? OrderId { get; set; }
    public int ProductId { get; set; }
    public virtual Product? Product { get; set; }
}