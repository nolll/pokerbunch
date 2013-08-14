using System.Collections.Generic;
using Core.Classes;

namespace Infrastructure.Data.Storage.Interfaces {

	public interface IPlayerStorage{

		Player GetPlayerById(Homegame homegame, int id);
		Player GetPlayerByName(Homegame homegame, string name);
		Player GetPlayerByUserName(Homegame homegame, string userName);
		List<Player> GetPlayers(Homegame homegame);
		int AddPlayer(Homegame homegame, string playerName);
		int AddPlayerWithUser(Homegame homegame, User user, int role);
        bool JoinHomegame(Player player, Homegame homegame, User user);
        bool DeletePlayer(Player player);

	}

}