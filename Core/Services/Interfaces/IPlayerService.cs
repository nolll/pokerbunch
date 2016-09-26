using System.Collections.Generic;
using Core.Entities;

namespace Core.Services
{
    public interface IPlayerService
    {
        IList<Player> GetList(string slug);
        IList<Player> Get(IList<int> ids);
        Player Get(int id);
        Player GetByUserId(string slug, int userId);
        int Add(Player player);
        bool JoinHomegame(Player player, Bunch bunch, int userId);
        void Delete(int playerId);
    }
}