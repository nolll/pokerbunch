using System.Collections.Generic;
using Core.Entities;

namespace Core.Services.Interfaces
{
    public interface ICashgameService
    {
        CashgameSuite GetSuite(Homegame homegame, int? year = null);
        IList<Player> GetPlayers(Cashgame cashgame);
        bool CashgameIsRunning(string bunchName);
        int? GetLatestYear(string slug);
    }
}