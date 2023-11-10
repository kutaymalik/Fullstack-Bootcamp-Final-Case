using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderAutomationsProject.Schema;

public class TokenRequest
{
    public string Email { get; set; }
    
    public string Password { get; set; }
}

public class TokenResponse
{
    public DateTime ExpireDate { get; set; }
    public string Token { get; set; }
    public string RefreshToken { get; set; }
    public string Email { get; set; }
    public string DealerNumber { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Role {  get; set; }
    public int DealerId {  get; set; }
}
