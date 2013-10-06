using Core.Classes;
using Infrastructure.Data.Classes;
using Infrastructure.Data.Storage;

namespace Infrastructure.Data.Factories
{
    public interface IRawCashgameFactory
    {
        RawCashgame Create(StorageDataReader reader);
        RawCashgame Create(Cashgame cashgame, GameStatus? status = null);
    }
}