using Microsoft.AspNetCore.Http;

namespace OrderAutomationsProject.Data.Helpers;

public interface ISessionService
{
    (int sessionId, string sessionRole) CheckSession();
}

public class SessionService : ISessionService
{
    private readonly IHttpContextAccessor httpContextAccessor;

    public SessionService(IHttpContextAccessor httpContextAccessor)
    {
        this.httpContextAccessor = httpContextAccessor;
    }

    public (int sessionId, string sessionRole) CheckSession()
    {
        var sessionIdClaim = httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == "Id");
         

        if (sessionIdClaim == null || !int.TryParse(sessionIdClaim.Value, out int sessionId))
        {
            throw new InvalidOperationException("Session id not found!");
        }

        var sessionRoleClaim = httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == "Role");

        if (sessionRoleClaim == null)
        {
            throw new InvalidOperationException("Session role not found!");
        }

        return (sessionId, sessionRoleClaim.Value);
    }
}
