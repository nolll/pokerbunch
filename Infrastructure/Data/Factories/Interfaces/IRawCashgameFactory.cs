using Core.Classes;
using Infrastructure.Data.Classes;
using Infrastructure.Data.Storage;

namespace Infrastructure.Data.Factories
{
    public interface IRawCashgameFactory
    {
        RawCashgameWithResults Create(StorageDataReader reader);
        RawCashgameWithResults Create(Cashgame cashgame, GameStatus? status = null);
    }
}