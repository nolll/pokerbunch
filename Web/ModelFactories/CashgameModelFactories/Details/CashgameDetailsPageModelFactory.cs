using System;
using Application.Services;
using Core.Entities;
using Web.ModelFactories.PageBaseModelFactories;
using Web.Models.CashgameModels.Details;
using Web.Models.UrlModels;

namespace Web.ModelFactories.CashgameModelFactories.Details
{
    public class CashgameDetailsPageModelFactory : ICashgameDetailsPageModelFactory
    {
        private readonly IPagePropertiesFactory _pagePropertiesFactory;
        private readonly ICashgameDetailsTableModelFactory _cashgameDetailsTableModelFactory;
        private readonly IGlobalization _globalization;

        public CashgameDetailsPageModelFactory(
            IPagePropertiesFactory pagePropertiesFactory, 
            ICashgameDetailsTableModelFactory cashgameDetailsTableModelFactory,
            IGlobalization globalization)
        {
            _pagePropertiesFactory = pagePropertiesFactory;
            _cashgameDetailsTableModelFactory = cashgameDetailsTableModelFactory;
            _globalization = globalization;
        }

        public CashgameDetailsPageModel Create(Homegame homegame, Cashgame cashgame, Player player, bool isManager)
        {
            var dateStr = cashgame.StartTime.HasValue ? _globalization.FormatShortDate(cashgame.StartTime.Value, true) : string.Empty;
            var showStartTime = cashgame.Status >= GameStatus.Running && cashgame.StartTime.HasValue;
            var showEndTime = cashgame.Status >= GameStatus.Finished && cashgame.EndTime != null;
            
            var model = new CashgameDetailsPageModel
                {
                    BrowserTitle = "Cashgame",
                    PageProperties = _pagePropertiesFactory.Create(homegame),
                    Heading = string.Format("Cashgame {0}", dateStr),
			        Location = cashgame.Location,
                    Duration = _globalization.FormatDuration(cashgame.Duration),
			        DurationEnabled = cashgame.Duration > 0,
                    ShowStartTime = showStartTime,
			        StartTime = showStartTime ? _globalization.FormatTime(TimeZoneInfo.ConvertTime(cashgame.StartTime.Value, homegame.Timezone)) : null,
			        ShowEndTime = showEndTime,
                    EndTime = showEndTime ? _globalization.FormatTime(TimeZoneInfo.ConvertTime(cashgame.EndTime.Value, homegame.Timezone)) : null,
                    Status = GameStatusName.GetName(cashgame.Status),
                    CashgameDetailsTableModel = _cashgameDetailsTableModelFactory.Create(homegame, cashgame),
                    EnableEdit = isManager,
                    EnableCheckpointsButton = cashgame.IsInGame(player.Id),
                    EditUrl = new EditCashgameUrl(homegame.Slug, cashgame.DateString),
                    CheckpointsUrl = new CashgameActionUrl(homegame.Slug, cashgame.DateString, player.Id),
                    ChartDataUrl = new CashgameDetailsChartJsonUrl(homegame.Slug, cashgame.DateString)
                };

            return model;
        }
    }
}