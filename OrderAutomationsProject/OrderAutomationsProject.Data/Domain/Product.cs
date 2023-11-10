using OrderAutomationsProject.Base.Model;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrderAutomationsProject.Data.Domain;

[Table("Product", Schema = "dbo")]
public class Product : BaseModel
{
    public string ProductName { get; set; }
    public string ProductDescription { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal Price { get; set; }
    public int StockQuantity { get; set; }
    public int CategoryId { get; set; }
    public virtual Category Category { get; set; }
}
