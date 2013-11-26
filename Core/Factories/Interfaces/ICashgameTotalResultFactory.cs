using System.Collections.Generic;
using Core.Classes;

namespace Core.Factories{

    public interface ICashgameTotalResultFactory{

        IList<CashgameTotalResult> CreateList(IEnumerable<Player> players, IDictionary<int, IList<CashgameResult>> resultIndex);

    }

}