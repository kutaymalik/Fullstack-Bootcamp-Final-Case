namespace OrderAutomationsProject.Schema;

public class AddressRequest
{
    public string AddressLine1 { get; set; }
    public string AddressLine2 { get; set; }
    public string City { get; set; }
    public string County { get; set; }
    public string PostalCode { get; set; }
}

public class AddressResponse
{
    public int Id { get; set; }
    public int DealerId { get; set; }
    public string AddressLine1 { get; set; }
    public string AddressLine2 { get; set; }
    public string City { get; set; }
    public string County { get; set; }
    public string PostalCode { get; set; }
    public string DealerName { get; set; }
}