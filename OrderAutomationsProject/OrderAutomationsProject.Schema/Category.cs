using OrderAutomationsProject.Data.Domain;

namespace OrderAutomationsProject.Schema;

public class CategoryRequest
{
    public string Name { get; set; }
    public string Description { get; set; }
}

public class CategoryResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public List<Product> Products { get; set; }
}