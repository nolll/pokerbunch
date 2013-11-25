using Core.Classes;

namespace Infrastructure.Services
{
    public interface ICashgameService
    {
        CashgameSuite GetSuite(Homegame homegame, int? year = null);
    }
}