using System.Security.Principal;
using System.Web.Security;

namespace Web.Security
{
    public class UserIdentity : IUserIdentity
    {
        public string UserName { get; }
        public string ApiToken { get; }
        public bool IsAuthenticated => !string.IsNullOrEmpty(UserName) && !string.IsNullOrEmpty(ApiToken);

        public UserIdentity(IPrincipal p)
        {
            if (p?.Identity != null && p.Identity.IsAuthenticated)
            {
                UserName = p.Identity.Name;
                ApiToken = GetToken(p);
            }
        }

        private string GetToken(IPrincipal p)
        {
            var formsIdentity = (FormsIdentity)p.Identity;
            return formsIdentity.Ticket.UserData;
        }
    }
}