using System.Collections.Generic;
using Core.Classes;

namespace Infrastructure.Data.Storage.Interfaces {

	public interface IPlayerStorage{

        Player GetPlayerById(int homegameId, int id);
        Player GetPlayerByName(int homegameId, string name);
        Player GetPlayerByUserName(int homegameId, string userName);
        List<Player> GetPlayers(int homegameId);
        int AddPlayer(int homegameId, string playerName);
        int AddPlayerWithUser(int homegameId, int userId, int role);
        bool JoinHomegame(int playerId, int role, int homegameId, int userId);
        bool DeletePlayer(int playerId);

	}

}