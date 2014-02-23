using System.Collections.Generic;
using Core.Classes;
using Infrastructure.Data.Classes;

namespace Infrastructure.Data.Mappers
{
    public interface ICashgameDataMapper
    {
        Cashgame Create(RawCashgame rawGame, IEnumerable<RawCheckpoint> checkpoints);
        IList<Cashgame> CreateList(IEnumerable<RawCashgame> rawGames, IEnumerable<RawCheckpoint> checkpoints);
    }
}