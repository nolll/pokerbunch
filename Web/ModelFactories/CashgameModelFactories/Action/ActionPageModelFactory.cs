using System.Collections.Generic;
using System.Linq;
using Application.Services;
using Core.Entities;
using Core.Entities.Checkpoints;
using Web.ModelFactories.PageBaseModelFactories;
using Web.Models.CashgameModels.Action;

namespace Web.ModelFactories.CashgameModelFactories.Action
{
    public class ActionPageModelFactory : IActionPageModelFactory
    {
        private readonly IPagePropertiesFactory _pagePropertiesFactory;
        private readonly IUrlProvider _urlProvider;
        private readonly ICheckpointModelFactory _checkpointModelFactory;
        private readonly IGlobalization _globalization;

        public ActionPageModelFactory(
            IPagePropertiesFactory pagePropertiesFactory,
            IUrlProvider urlProvider,
            ICheckpointModelFactory checkpointModelFactory,
            IGlobalization globalization)
        {
            _pagePropertiesFactory = pagePropertiesFactory;
            _urlProvider = urlProvider;
            _checkpointModelFactory = checkpointModelFactory;
            _globalization = globalization;
        }

        public ActionPageModel Create(Homegame homegame, Cashgame cashgame, Player player, CashgameResult result, Role role)
        {
            var dateString = cashgame.StartTime.HasValue ? _globalization.FormatShortDate(cashgame.StartTime.Value, true) : string.Empty;

            return new ActionPageModel
                {
                    BrowserTitle = "Player Actions",
                    PageProperties = _pagePropertiesFactory.Create(homegame),
                    Heading = string.Format("Cashgame {0}, {1}", dateString, player.DisplayName),
                    Checkpoints = GetCheckpointModels(homegame, cashgame, result, player, role),
                    ChartDataUrl = _urlProvider.GetCashgameActionChartJsonUrl(homegame.Slug, cashgame.DateString, player.DisplayName)
                };
        }

        private List<CheckpointModel> GetCheckpointModels(Homegame homegame, Cashgame cashgame, CashgameResult result, Player player, Role role)
        {
            var checkpoints = GetCheckpoints(result);
            return checkpoints.Select(checkpoint => _checkpointModelFactory.Create(homegame, cashgame, player, checkpoint, role)).ToList();
        }

        private IEnumerable<Checkpoint> GetCheckpoints(CashgameResult result)
        {
            return PlayerIsInGame(result) ? result.Checkpoints : new List<Checkpoint>();
        }

        private bool PlayerIsInGame(CashgameResult result)
        {
            return result != null;
        }

    }
}