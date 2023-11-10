using OrderAutomationsProject.Base.Model;
using OrderAutomationsProject.Base.PaymentType;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrderAutomationsProject.Data.Domain;

[Table("Payment", Schema = "dbo")]
public class Payment : BaseModel
{
    public PaymentTypeName PaymentType { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal PaymentAmount { get; set; }
    public DateTime PaymentDate { get; set; }
    public string PaymentDescription { get; set; }
    public int? CardId { get; set; }
    public Card? Card { get; set; }
    public int? OrderId { get; set; }
    public Order Order { get; set; }
}
