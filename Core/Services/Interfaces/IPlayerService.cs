using Core.Entities;

namespace Core.Services
{
	public interface IPlayerService
    {
        Player Get(string id);
        void Delete(string playerId);
        void Invite(string playerId, string email);
    }
}