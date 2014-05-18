using Core.Entities;

namespace Application.Factories
{
    public class PlayerFactory : IPlayerFactory
    {
        public Player Create(int id, int userId, string displayName, Role role)
        {
            return new Player(id, userId, displayName, role);
        }
    }
}