using System.Collections.Generic;
using Core.Entities;

namespace Core.Services
{
    public interface IPlayerService
    {
        IList<Player> GetList(string slug);
        IList<Player> Get(IList<string> ids);
        Player Get(string id);
        Player GetByUserId(string slug, string userId);
        string Add(Player player);
        bool JoinHomegame(Player player, Bunch bunch, string userId);
        void Delete(string playerId);
    }
}