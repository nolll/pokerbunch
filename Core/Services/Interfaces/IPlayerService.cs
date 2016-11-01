using System.Collections.Generic;
using Core.Entities;

namespace Core.Services
{
    public interface IPlayerService
    {
        IList<Player> List(string slug);
        IList<Player> Get(IList<string> ids);
        Player Get(string id);
        Player GetByUser(string slug, string userId);
        string Add(Player player);
        bool JoinBunch(Player player, Bunch bunch, string userId);
        void Delete(string playerId);
    }
}