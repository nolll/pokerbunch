using System.Collections.Generic;
using Core.Entities;

namespace Core.Services
{
	public interface IPlayerService
    {
        Player Get(string id);

	    IList<Player> List(string bunchId);

        string Add(Player player);
		void Delete(string playerId);

	    void Invite(string playerId, string email);
    }
}