using System.Web;
using Application.Services;
using Core.Classes;
using Web.Models.CashgameModels.Matrix;

namespace Web.ModelFactories.CashgameModelFactories.Matrix
{
    public class CashgameMatrixTableRowModelFactory : ICashgameMatrixTableRowModelFactory
    {
        private readonly IUrlProvider _urlProvider;
        private readonly ICashgameMatrixTableCellModelFactory _cashgameMatrixTableCellModelFactory;
        private readonly IResultFormatter _resultFormatter;
        private readonly IGlobalization _globalization;

        public CashgameMatrixTableRowModelFactory(
            IUrlProvider urlProvider,
            ICashgameMatrixTableCellModelFactory cashgameMatrixTableCellModelFactory,
            IResultFormatter resultFormatter,
            IGlobalization globalization)
        {
            _urlProvider = urlProvider;
            _cashgameMatrixTableCellModelFactory = cashgameMatrixTableCellModelFactory;
            _resultFormatter = resultFormatter;
            _globalization = globalization;
        }

        public CashgameMatrixTableRowModel Create(Homegame homegame, CashgameSuite suite, Player player, CashgameTotalResult result, int rank)
        {
            var cellModels = _cashgameMatrixTableCellModelFactory.CreateList(suite.Cashgames, player);
            
            return new CashgameMatrixTableRowModel
                {
                    Rank = rank,
                    Name = player.DisplayName,
                    UrlEncodedName = HttpUtility.UrlPathEncode(player.DisplayName),
                    PlayerUrl = _urlProvider.GetPlayerDetailsUrl(homegame.Slug, player.DisplayName),
                    CellModels = cellModels,
                    TotalResult = _globalization.FormatResult(homegame.Currency, result.Winnings),
                    ResultClass = _resultFormatter.GetWinningsCssClass(result.Winnings)
                };
        }
    }
}