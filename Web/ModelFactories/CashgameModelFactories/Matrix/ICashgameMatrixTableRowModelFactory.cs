using Core.Classes;
using Web.Models.CashgameModels.Matrix;

namespace Web.ModelFactories.CashgameModelFactories.Matrix
{
    public interface ICashgameMatrixTableRowModelFactory
    {
        CashgameMatrixTableRowModel Create(Homegame homegame, CashgameSuite suite, CashgameTotalResult result, int rank);
    }
}