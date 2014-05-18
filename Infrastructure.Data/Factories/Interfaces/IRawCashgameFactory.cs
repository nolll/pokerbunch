using Core.Entities;
using Infrastructure.Data.Classes;
using Infrastructure.Data.Interfaces;

namespace Infrastructure.Data.Factories
{
    public interface IRawCashgameFactory
    {
        RawCashgame Create(IStorageDataReader reader);
        RawCashgame Create(Cashgame cashgame, GameStatus? status = null);
    }
}