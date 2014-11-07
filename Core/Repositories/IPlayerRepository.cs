using System.Collections.Generic;
using Core.Entities;

namespace Core.Repositories
{
	public interface IPlayerRepository
    {
		IList<Player> GetList(int bunchId);
        IList<Player> GetList(IList<int> ids);
        Player GetById(int id);
		Player GetByName(int bunchId, string name);
        Player GetByUserId(int bunchId, int userId);
        int Add(Player player);
		bool JoinHomegame(Player player, Bunch bunch, User user);
		bool Delete(int playerId);
	}
}