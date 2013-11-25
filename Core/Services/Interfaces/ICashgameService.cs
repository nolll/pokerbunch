using Core.Classes;

namespace Core.Services
{
    public interface ICashgameService
    {
        CashgameSuite GetSuite(Homegame homegame, int? year = null);
        CashgameFacts GetFacts(Homegame homegame, int? year = null);
    }
}