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

        public static void RequirePlayer(User user, Player player)
        {
            RequireRole(user, player, Role.Player);
        }

        public static void RequireManager(User user, Player player)
        {
            RequireRole(user, player, Role.Manager);
        }

        public static void RequireAdmin(User user)
        {
            if(!user.IsAdmin)
                throw new AccessDeniedException();
        }

        public static void RequireMe(User user, Player player, int playerId)
        {
            if(!user.IsAdmin && playerId != player.Id)
                throw new AccessDeniedException();
        }
        
        private static void RequireRole(User user, Player player, Role role)
        {
            if(!IsInRole(user, player, role))
                throw new AccessDeniedException();
        }
    }
}