using System.Collections.Generic;
using Core.Entities;

namespace Core.Repositories
{
	public interface IPlayerRepository
    {
        Player Get(string id);

	    IList<Player> List(string bunchId);
        Player GetByUser(string bunchId, string userId);

        bool JoinBunch(Player player, Bunch bunch, string userId);
        string Add(Player player);
		void Delete(string playerId);

	    void Invite(string playerId, string email);
    }
}