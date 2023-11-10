using OrderAutomationsProject.Base.Model;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrderAutomationsProject.Data.Domain;

public class Cost : BaseModel
{
    public string CostDescription { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal CostAmount { get; set; }
    public bool? Confirmation {  get; set; } 
    public int DealerId { get; set; }
    public virtual Dealer? Dealer { get; set; }
}
