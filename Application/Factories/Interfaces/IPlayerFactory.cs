using Core.Entities;

namespace Application.Factories
{
    public interface IPlayerFactory
    {
        Player Create(int id, int userId, string displayName, Role role);
    }
}