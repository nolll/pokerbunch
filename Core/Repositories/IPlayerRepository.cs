using System.Collections.Generic;
using Core.Entities;

namespace Core.Repositories {

	public interface IPlayerRepository{

		IList<Player> GetList(Bunch bunch);
        IList<Player> GetList(IList<int> ids);
        Player GetById(int id);
		Player GetByName(Bunch bunch, string name);
        Player GetByUserName(Bunch bunch, string userName);
		int Add(Bunch bunch, string playerName);
		int Add(Bunch bunch, User user, Role role);
		bool JoinHomegame(Player player, Bunch bunch, User user);
		bool Delete(Bunch bunch, Player player);
	}

}