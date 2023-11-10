using OrderAutomationsProject.Base.Model;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrderAutomationsProject.Data.Domain;

[Table("Bill", Schema = "dbo")]
public class Bill : BaseModel
{
    public DateTime BillDate { get; set; }
    public string DealerName { get; set; }
    public string DealerAddress { get; set; }
    public string DealerEmail { get; set; }
    public string DealerPhone { get; set; }
    public string OrderNumber { get; set; }
    public int OrderId { get; set; }
    public Order Order { get; set; }
    public List<OrderItem> OrderItems { get; set; }
    public decimal TotalAmount { get; set; }
}
