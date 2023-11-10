namespace OrderAutomationsProject.Schema;

public class CostRequest
{
    public string CostDescription { get; set; }
    public decimal CostAmount { get; set; }
    public bool? Confirmation { get; set; }
}

public class CostResponse
{
    public int Id { get; set; }
    public string CostDescription { get; set; }
    public decimal CostAmount { get; set; }
    public bool? Confirmation { get; set; }
    public int DealerId { get; set; }
    public string DealerName { get; set; }
}

