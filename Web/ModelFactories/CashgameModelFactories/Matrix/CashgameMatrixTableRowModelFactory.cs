using System.Collections.Generic;
using System.Web;
using Application.Services;
using Core.Classes;
using Core.Repositories;
using Web.Models.CashgameModels.Matrix;

namespace Web.ModelFactories.CashgameModelFactories.Matrix
{
    public class CashgameMatrixTableRowModelFactory : ICashgameMatrixTableRowModelFactory
    {
        private readonly IUrlProvider _urlProvider;
        private readonly ICashgameMatrixTableCellModelFactory _cashgameMatrixTableCellModelFactory;
        private readonly IResultFormatter _resultFormatter;
        private readonly IGlobalization _globalization;
        private readonly IPlayerRepository _playerRepository;

        public CashgameMatrixTableRowModelFactory(
            IUrlProvider urlProvider,
            ICashgameMatrixTableCellModelFactory cashgameMatrixTableCellModelFactory,
            IResultFormatter resultFormatter,
            IGlobalization globalization,
            IPlayerRepository playerRepository)
        {
            _urlProvider = urlProvider;
            _cashgameMatrixTableCellModelFactory = cashgameMatrixTableCellModelFactory;
            _resultFormatter = resultFormatter;
            _globalization = globalization;
            _playerRepository = playerRepository;
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

        public List<CashgameMatrixTableRowModel> CreateList(Homegame homegame, CashgameSuite suite, IEnumerable<CashgameTotalResult> results)
        {
            var models = new List<CashgameMatrixTableRowModel>();
            var rank = 0;
            foreach (var result in results)
            {
                rank++;
                var player = _playerRepository.GetById(result.PlayerId);
                models.Add(Create(homegame, suite, player, result, rank));
            }
            return models;
        }
    }
}