using System.Collections.Generic;
using Core.Entities;
using Web.Models.CashgameModels.Matrix;

namespace Web.ModelFactories.CashgameModelFactories.Matrix
{
    public interface ICashgameMatrixTableRowModelFactory
    {
        CashgameMatrixTableRowModel Create(Homegame homegame, CashgameSuite suite, Player player, CashgameTotalResult result, int rank);
        List<CashgameMatrixTableRowModel> CreateList(Homegame homegame, CashgameSuite suite, IEnumerable<CashgameTotalResult> results);
    }
}