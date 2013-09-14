using System.Collections.Generic;
using System.Linq;
using Core.Classes;
using Core.Classes.Checkpoints;
using Infrastructure.System;
using Web.ModelFactories.PageBaseModelFactories;
using Web.Models.CashgameModels.Action;
using Web.Models.UrlModels;

namespace Web.ModelFactories.CashgameModelFactories
{
    public class ActionPageModelFactory : IActionPageModelFactory
    {
        private readonly IPagePropertiesFactory _pagePropertiesFactory;

        public ActionPageModelFactory(IPagePropertiesFactory pagePropertiesFactory)
        {
            _pagePropertiesFactory = pagePropertiesFactory;
        }

        public ActionPageModel Create(User user, Homegame homegame, Cashgame cashgame, Player player, CashgameResult result, Role role, List<int> years = null, Cashgame runningGame = null)
        {
            var dateString = cashgame.StartTime.HasValue ? Globalization.FormatShortDate(cashgame.StartTime.Value, true) : string.Empty;

            return new ActionPageModel
                {
                    BrowserTitle = "Player Actions",
                    PageProperties = _pagePropertiesFactory.Create(user, homegame, runningGame),
                    Heading = string.Format("Cashgame {0}, {1}", dateString, player.DisplayName),
                    Checkpoints = GetCheckpointModels(homegame, cashgame, result, player, role),
                    ChartDataUrl = new CashgameActionChartJsonUrlModel(homegame, cashgame, player)
                };
        }

        private List<CheckpointModel> GetCheckpointModels(Homegame homegame, Cashgame cashgame, CashgameResult result, Player player, Role role)
        {
            var checkpoints = GetCheckpoints(result);
            return checkpoints.Select(checkpoint => new CheckpointModel(homegame, cashgame, player, checkpoint, role)).ToList();
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