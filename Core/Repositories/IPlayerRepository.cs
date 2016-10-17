using System.Collections.Generic;
using Core.Entities;

namespace Core.Repositories
{
	public interface IPlayerRepository
    {
        Player Get(string id);
        IList<Player> Get(IList<string> ids);

	    IList<string> Find(string bunchId);
        IList<string> FindByName(string bunchId, string name);
        IList<string> FindByUserId(string bunchId, string userId);

        bool JoinHomegame(Player player, Bunch bunch, string userId);
        string Add(Player player);
		void Delete(string playerId);
	}
}