using System.Collections.Generic;
using Core.Entities;

namespace Core.Services
{
    public interface ICashgameService
    {
        CashgameSuite GetSuite(int bunchId, int? year = null);
        IList<Player> GetPlayers(Cashgame cashgame);
    }
}