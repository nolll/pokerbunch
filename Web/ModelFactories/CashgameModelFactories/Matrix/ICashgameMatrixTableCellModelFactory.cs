using Core.Classes;
using Web.Models.CashgameModels.Matrix;

namespace Web.ModelFactories.CashgameModelFactories.Matrix
{
    public interface ICashgameMatrixTableCellModelFactory
    {
        CashgameMatrixTableCellModel Create(Cashgame cashgame, CashgameResult result);
    }
}