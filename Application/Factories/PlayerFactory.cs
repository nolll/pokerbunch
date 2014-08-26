using Core.Entities;

namespace Application.Factories
{
    public static class PlayerFactory
    {
        public static Player Create(int id, int userId, string displayName, Role role)
        {
            return new Player(id, userId, displayName, role);
        }
    }
}