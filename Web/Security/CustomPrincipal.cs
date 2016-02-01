using System.Security.Principal;

namespace Web.Security
{
    public class CustomPrincipal : IPrincipal
    {
        public IIdentity Identity { get; }

        public CustomPrincipal(IIdentity identity)
        {
            Identity = identity;
        }

        public bool IsInRole(string role)
        {
            return false;
        }
    }
}