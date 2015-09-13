using System.Collections.Generic;
using Infrastructure.Storage.Classes;

namespace Infrastructure.Storage.Interfaces
{
	public interface IPlayerStorage
    {
        RawPlayer GetPlayerById(int id);
        IList<int> GetPlayerIdsByName(int homegameId, string name);
        IList<int> GetPlayerIdsByUserId(int bunchId, int userId);
        IList<RawPlayer> GetPlayerList(IList<int> playerIds);
        IList<int> GetPlayerIdList(int homegameId);
        int AddPlayer(RawPlayer player);
        bool JoinHomegame(int playerId, int role, int homegameId, int userId);
        bool DeletePlayer(int playerId);
	}
}