using Core.Classes;
using Web.Models.CashgameModels.Matrix;

namespace Web.ModelFactories.CashgameModelFactories.Matrix
{
    public interface ICashgameMatrixTableColumnHeaderModelFactory
    {
        CashgameMatrixTableColumnHeaderModel Create(Homegame homegame, Cashgame cashgame, bool showYear = false);
    }
}