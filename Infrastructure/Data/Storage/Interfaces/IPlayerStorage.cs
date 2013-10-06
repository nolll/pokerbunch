using System.Collections.Generic;
using Infrastructure.Data.Classes;

namespace Infrastructure.Data.Storage.Interfaces {

	public interface IPlayerStorage{

        RawPlayer GetPlayerById(int homegameId, int id);
        RawPlayer GetPlayerByName(int homegameId, string name);
        RawPlayer GetPlayerByUserName(int homegameId, string userName);
        IList<RawPlayer> GetPlayers(int homegameId);
        int AddPlayer(int homegameId, string playerName);
        int AddPlayerWithUser(int homegameId, int userId, int role);
        bool JoinHomegame(int playerId, int role, int homegameId, int userId);
        bool DeletePlayer(int playerId);

	}

}