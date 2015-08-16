using Core.Entities;
using Core.Exceptions;

namespace Core.Services
{
    public static class RoleHandler
    {
        public static bool IsInRole(User user, Player player, Role role)
        {
            return user.IsAdmin || player.IsInRole(role);
        }

        public static void RequireRole(User user, Player player, Role role)
        {
            if(!IsInRole(user, player, role))
                throw new AccessDeniedException();
        }
    }
}