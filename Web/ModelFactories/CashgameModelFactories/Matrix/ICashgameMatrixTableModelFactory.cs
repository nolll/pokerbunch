using Core.Classes;
using Web.Models.CashgameModels.Matrix;

namespace Web.ModelFactories.CashgameModelFactories.Matrix
{
    public interface ICashgameMatrixTableModelFactory
    {
        CashgameMatrixTableModel Create(Homegame homegame, CashgameSuite suite);
    }
}