using System.Security.Principal;
using Microsoft.AspNetCore.Http;

namespace Web.Bootstrapping;

public class CurrentUser
{
    private readonly IHttpContextAccessor _context;
    public CurrentUser(IHttpContextAccessor context)
    {
        _context = context;
    }

    public IIdentity Identity => _context.HttpContext.User?.Identity;
    public string UserName => Identity?.Name;
}