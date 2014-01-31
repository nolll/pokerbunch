using System.Collections.Generic;
using Core.Classes;

namespace Application.Services
{
    public interface ICashgameService
    {
        CashgameSuite GetSuite(Homegame homegame, int? year = null);
        CashgameFacts GetFacts(Homegame homegame, int? year = null);
        IList<Player> GetPlayers(Cashgame cashgame);
        bool CashgameIsRunning(string bunchName);
        int? GetLatestYear(string slug);
    }
}