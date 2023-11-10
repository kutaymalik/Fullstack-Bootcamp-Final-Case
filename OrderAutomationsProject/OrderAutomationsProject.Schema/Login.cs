using OrderAutomationsProject.Data.Domain;

namespace OrderAutomationsProject.Schema;

public class LoginRequest
{
    public string Email { get; set; }
    public string Password { get; set; }
}

public class LoginResponse
{
    public Dealer dealer { get; set; }
    public string Token { get; set; }
}
