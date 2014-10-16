using System.Collections.Generic;
using Infrastructure.Data.Classes;

namespace Infrastructure.Data {

	public interface IPlayerStorage{

        RawPlayer GetPlayerById(int id);
        int? GetPlayerIdByName(int homegameId, string name);
        int? GetPlayerIdByUserName(int homegameId, string userName);
        IList<RawPlayer> GetPlayerList(IList<int> playerIds);
        IList<int> GetPlayerIdList(int homegameId);
        int AddPlayer(int homegameId, string playerName);
        int AddPlayerWithUser(int homegameId, int userId, int role);
        bool JoinHomegame(int playerId, int role, int homegameId, int userId);
        bool DeletePlayer(int playerId);

	}

}