using System.Collections.Generic;
using Core.Entities;

namespace Core.Repositories
{
	public interface IPlayerRepository
    {
        Player Get(int id);
        IList<Player> Get(IList<int> ids);

	    IList<int> Find(int bunchId);
        IList<int> Find(int bunchId, string name);
        IList<int> Find(int bunchId, int userId);
        IList<Player> GetList(int bunchId);
        bool JoinHomegame(Player player, Bunch bunch, int userId);
        
        int Add(Player player);
		bool Delete(int playerId);
	}
}