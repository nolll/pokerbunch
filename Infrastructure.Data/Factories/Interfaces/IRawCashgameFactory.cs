using Core.Classes;
using Infrastructure.Data.Classes;
using Infrastructure.Data.Interfaces;

namespace Infrastructure.Data.Factories.Interfaces
{
    public interface IRawCashgameFactory
    {
        RawCashgameWithResults Create(IStorageDataReader reader);
        RawCashgameWithResults Create(Cashgame cashgame, GameStatus? status = null);
    }
}