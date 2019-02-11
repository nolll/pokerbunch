using System.Collections.Generic;
using System.Linq;
using Core.UseCases;
using JetBrains.Annotations;
using PokerBunch.Common.Urls.SiteUrls;

namespace Web.Models.CashgameModels.Running
{
    public class RunningCashgameJsonModel : RunningCashgameRefreshJsonModel
    {
        [UsedImplicitly]
        public string RefreshUrl { get; private set; }

        [UsedImplicitly]
        public string LocationUrl { get; private set; }

        [UsedImplicitly]
        public string LocationName { get; private set; }

        public RunningCashgameJsonModel(RunningCashgame.Result result) : base(result)
        {
            RefreshUrl = new RunningCashgamePlayersJsonUrl(result.Slug).Relative;
            LocationUrl = new LocationDetailsUrl(result.LocationId).Relative;
            LocationName = result.LocationName;
        }
    }

    public class RunningCashgameRefreshJsonModel
    {
        [UsedImplicitly]
        public List<RunningCashgamePlayerJsonModel> Players { get; private set; }

        public RunningCashgameRefreshJsonModel(RunningCashgame.Result result)
        {
            Players = result.PlayerItems.Select(o => new RunningCashgamePlayerJsonModel(o)).ToList();
        }
    }
}