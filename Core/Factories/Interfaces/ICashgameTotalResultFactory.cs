using System.Collections.Generic;
using Core.Entities;

namespace Core.Factories.Interfaces
{
    public interface ICashgameTotalResultFactory
    {
        IList<CashgameTotalResult> CreateList(IList<Player> players, IList<Cashgame> cashgames);
    }
}