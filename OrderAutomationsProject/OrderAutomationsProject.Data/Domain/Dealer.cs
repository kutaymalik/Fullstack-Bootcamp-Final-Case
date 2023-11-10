using OrderAutomationsProject.Base.Model;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrderAutomationsProject.Data.Domain;

[Table("Dealer", Schema = "dbo")]
public class Dealer : BaseModel
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public string Role { get; set; }
    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenExpireDate { get; set; }
    public string DealerNumber { get; set; }
    public DateTime DateOfBirth { get; set; } //ddmmyy
    public string PhoneNumber { get; set; }
    public decimal Dividend { get; set; }
    public decimal OpenAccountLimit { get; set; }
    public int? PasswordRetryCount { get; set; }
    public virtual List<Address>? Addresses { get; set; }
    public virtual List<Order>? Orders { get; set; }
    public virtual List<Card>? Cards { get; set; }
    public virtual List<Bill>? Bills { get; set; }
    public virtual List<Cost>? Costs { get; set; }
    public virtual List<Payment>? Payments { get; set; }
    public virtual List<Message>? MessagesReceived { get; set; }
    public virtual List<Message>? MessagesSent { get; set; }
}
