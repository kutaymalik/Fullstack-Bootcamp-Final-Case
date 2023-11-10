using OrderAutomationsProject.Data.Domain;

namespace OrderAutomationsProject.Schema;

public class ReportRequest
{
    public string TimePeriod { get; set; }
}

public class ReportResponse
{
    public List<Order> Orders { get; set; }
    public decimal OrderIntensity { get; set; }
}

public class DealerBasedReportRequest
{
    public int DealerId { get; set; }
    public string TimePeriod { get; set; }
}
