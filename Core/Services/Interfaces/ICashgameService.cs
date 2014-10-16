using System.Collections.Generic;
using Core.Entities;

namespace Core.Services
{
    public interface ICashgameService
    {
        CashgameSuite GetSuite(Bunch bunch, int? year = null);
        IList<Player> GetPlayers(Cashgame cashgame);
    }
}