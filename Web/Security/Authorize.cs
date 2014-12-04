using System.Linq;
using System.Security.Principal;
using Core;
using Core.Entities;

namespace Web.Security
{
    public static class Authorize
    {
        public static bool Bunch(IPrincipal user, string slug, Role role)
        {
            var identity = user.Identity as CustomIdentity;
            if (identity == null)
                return false;
            if (role == Role.Admin && identity.IsAdmin)
                return true;
            return identity.Bunches.Any(userBunch => userBunch.Slug == slug && userBunch.Role >= role);
        }

        public static bool SpecificUser(IPrincipal user, string userName)
        {
            var identity = user.Identity as CustomIdentity;
            if (identity == null)
                return false;
            return identity.IsAdmin || identity.UserName.ToLower() == userName;
        }

        public static bool SpecificPlayer(IPrincipal user, string slug, int playerId)
        {
            var identity = user.Identity as CustomIdentity;
            if (identity == null)
                return false;
            return identity.IsAdmin || identity.Bunches.Any(userBunch => userBunch.Slug == slug && userBunch.Id == playerId);
        }

        public static bool Admin(IPrincipal user)
        {
            var identity = user.Identity as CustomIdentity;
            if (identity == null)
                return false;
            return identity.IsAdmin;
        }
    }
}