using OrderAutomationsProject.Base.Model;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrderAutomationsProject.Data.Domain;

[Table("Category", Schema = "dbo")]
public class Category : BaseModel
{
    public string Name { get; set; }
    public string Description { get; set; }
    public virtual List<Product>? Products { get; set; }
}