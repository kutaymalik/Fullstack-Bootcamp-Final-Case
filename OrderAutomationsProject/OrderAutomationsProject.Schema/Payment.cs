using OrderAutomationsProject.Base.PaymentType;
using OrderAutomationsProject.Data.Domain;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrderAutomationsProject.Schema;

public class PaymentRequest
{
    public string PaymentType { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public string PaymentDescription { get; set; }
    public string CardNumber { get; set; }
    public int? OrderId { get; set; }
}

public class PaymentResponse
{
    public int Id { get; set; }
    public PaymentTypeName PaymentType { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal PaymentAmount { get; set; }
    public DateTime PaymentDate { get; set; }
    public string PaymentDescription { get; set; }
    public Order? Order { get; set; }
    public Card? Card { get; set; }
}
