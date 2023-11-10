using OrderAutomationsProject.Data.Domain;

namespace OrderAutomationsProject.Schema;

public class DealerRequest
{
    public string Role {  get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public DateTime DateOfBirth { get; set; } //ddmmyy
    public string PhoneNumber { get; set; }
    public decimal Dividend { get; set; }
    public decimal OpenAccountLimit { get; set; }
}

public class DealerResponse
{
    public int Id { get; set; }
    public string DealerNumber { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public string Role { get; set; }
    public DateTime DateOfBirth { get; set; } //ddmmyy
    public string PhoneNumber { get; set; }
    public int PasswordRetryCount { get; set; }
    public decimal Dividend { get; set; }
    public decimal OpenAccountLimit { get; set; }
    public string RefreshToken { get; set; }
    public string RefreshTokenExpireDate { get; set; }
    public virtual List<Address>? Addresses { get; set; }
    public virtual List<Order>? Orders { get; set; }
    public virtual List<Cost> Costs { get; set; }
}

public class DealerUpdateRequest
{
    public string? Role { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public string? PasswordHash { get; set; }
    public DateTime? DateOfBirth { get; set; } //ddmmyy
    public string? PhoneNumber { get; set; }
    public decimal? Dividend { get; set; }
    public decimal? OpenAccountLimit { get; set; }
}
