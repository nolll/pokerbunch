using System.Collections.Generic;
using Application.Services;
using Application.Urls;
using Core.Entities;
using Web.ModelFactories.CashgameModelFactories.Matrix;

namespace Web.Models.CashgameModels.Matrix
{
    public class CashgameMatrixTableRowModel
    {
	    public int Rank { get; private set; }
	    public string Name { get; private set; }
	    public string TotalResult { get; private set; }
	    public string ResultClass { get; private set; }
	    public Url PlayerUrl { get; private set; }
        public IList<CashgameMatrixTableCellModel> CellModels { get; private set; }

        public CashgameMatrixTableRowModel(Bunch bunch, CashgameSuite suite, Player player, CashgameTotalResult result, int rank)
        {
            Rank = rank;
            Name = player.DisplayName;
            PlayerUrl = new PlayerDetailsUrl(bunch.Slug, player.Id);
            CellModels = CreateCells(suite.Cashgames, player);
            TotalResult = Globalization.FormatResult(bunch.Currency, result.Winnings);
            ResultClass = ResultFormatter.GetWinningsCssClass(result.Winnings); ;
        }

        private static IList<CashgameMatrixTableCellModel> CreateCells(IEnumerable<Cashgame> cashgames, Player player)
        {
            var models = new List<CashgameMatrixTableCellModel>();
            if (cashgames != null)
            {
                foreach (var cashgame in cashgames)
                {
                    var result = cashgame.GetResult(player.Id);
                    models.Add(CreateCell(cashgame, result));
                }
            }
            return models;
        }

        private static CashgameMatrixTableCellModel CreateCell(Cashgame cashgame, CashgameResult result)
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
    }
}