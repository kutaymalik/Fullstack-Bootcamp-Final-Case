using OrderAutomationsProject.Base.Bank;
using OrderAutomationsProject.Base.CardHolder;
using OrderAutomationsProject.Base.Model;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrderAutomationsProject.Data.Domain;

[Table("Card", Schema = "dbo")]
public class Card : BaseModel
{
    public string CardNumber { get; set; }
    public string ExpiryDate { get; set; }
    public string CVV { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal ExpenseLimit { get; set; }
    public int DealerId { get; set; }
    public virtual Dealer Dealer { get; set; }
    public BankName BankName { get; set; }
    public CardHolderType CardHolderType { get; set; }
}
