using System.Collections.Generic;
using Core.Entities;

namespace Core.Repositories
{
	public interface IPlayerRepository
    {
        Player Get(string id);
        IList<Player> Get(IList<string> ids);

	    IList<string> Find(string slug);
        IList<string> FindByName(string slug, string name);
        IList<string> FindByUserId(string slug, string userId);

        bool JoinHomegame(Player player, Bunch bunch, string userId);
        string Add(Player player);
		void Delete(string playerId);
	}
}