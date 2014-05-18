using System.Collections.Generic;
using Core.Entities;

namespace Core.Repositories {

	public interface IPlayerRepository{

		IList<Player> GetList(Homegame homegame);
        IList<Player> GetList(IList<int> ids);
        Player GetById(int id);
		Player GetByName(Homegame homegame, string name);
        Player GetByUserName(Homegame homegame, string userName);
		int Add(Homegame homegame, string playerName);
		int Add(Homegame homegame, User user, Role role);
		bool JoinHomegame(Player player, Homegame homegame, User user);
		bool Delete(Homegame homegame, Player player);
	}

}