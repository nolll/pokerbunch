using System.Collections.Generic;
using Core.Classes;
using Web.Models.CashgameModels.Matrix;

namespace Web.ModelFactories.CashgameModelFactories.Matrix
{
    public interface ICashgameMatrixTableCellModelFactory
    {
        CashgameMatrixTableCellModel Create(Cashgame cashgame, CashgameResult result);
        IList<CashgameMatrixTableCellModel> CreateList(IEnumerable<Cashgame> cashgames, Player player);
    }
}