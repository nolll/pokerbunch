using Core.Entities;

namespace Core.Services
{
    public static class RoleHandler
    {
        public static bool IsInRole(User user, Player player, Role role)
        {
            return user.IsAdmin || player.IsInRole(role);
        }

        public static bool IsInRole(Role userRole, Role requiredRole)
        {
            return userRole == Role.Admin || userRole >= requiredRole;
        }
    }
}