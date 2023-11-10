using System.ComponentModel.DataAnnotations.Schema;
using OrderAutomationsProject.Base.Model;

namespace OrderAutomationsProject.Data.Domain;

[Table("Address", Schema = "dbo")]
public class Address : BaseModel
{
    [ForeignKey("Dealer")]
    public int DealerId { get; set; }
    public string AddressLine1 { get; set; }
    public string AddressLine2 { get; set; }
    public string City { get; set; }
    public string County { get; set; }
    public string PostalCode { get; set; }
    public virtual Dealer Dealer { get; set; }
}
