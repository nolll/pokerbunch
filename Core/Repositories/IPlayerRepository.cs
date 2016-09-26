using System.Collections.Generic;
using Core.Entities;

namespace Core.Repositories
{
	public interface IPlayerRepository
    {
        Player Get(int id);
        IList<Player> Get(IList<int> ids);

	    IList<int> Find(string slug);
        IList<int> Find(string slug, string name);
        IList<int> Find(string slug, int userId);

        bool JoinHomegame(Player player, Bunch bunch, int userId);
        int Add(Player player);
		void Delete(int playerId);
	}
}