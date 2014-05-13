using System.Collections.Generic;
using Application.Services;
using Core.Classes;
using Web.Models.CashgameModels.Matrix;

namespace Web.ModelFactories.CashgameModelFactories.Matrix
{
    public class CashgameMatrixTableCellModelFactory : ICashgameMatrixTableCellModelFactory
    {
        public CashgameMatrixTableCellModel Create(Cashgame cashgame, CashgameResult result)
        {
            if (result == null)
            {
                return new CashgameMatrixTableCellModel
                    {
                        ShowResult = false
                    };
            }

            var hasBestResult = cashgame.IsBestResult(result);

            return new CashgameMatrixTableCellModel
                {
                    ShowResult = true,
                    ShowTransactions = result.Buyin > 0,
                    Buyin = result.Buyin,
                    Cashout = result.Stack,
                    Winnings = ResultFormatter.FormatWinnings(result.Winnings),
                    ResultClass = ResultFormatter.GetWinningsCssClass(result.Winnings),
                    HasBestResult = hasBestResult,
                    WinnerClass = hasBestResult ? "winner" : null
                };
        }

        public IList<CashgameMatrixTableCellModel> CreateList(IEnumerable<Cashgame> cashgames, Player player)
        {
            var models = new List<CashgameMatrixTableCellModel>();
            if (cashgames != null)
            {
                foreach (var cashgame in cashgames)
                {
                    var result = cashgame.GetResult(player.Id);
                    models.Add(Create(cashgame, result));
                }
            }
            return models;
        }
    }
}