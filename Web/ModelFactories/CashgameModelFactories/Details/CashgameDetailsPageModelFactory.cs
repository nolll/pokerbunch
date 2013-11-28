using System;
using Core.Classes;
using Core.Services;
using Infrastructure.System;
using Web.ModelFactories.PageBaseModelFactories;
using Web.Models.CashgameModels.Details;

namespace Web.ModelFactories.CashgameModelFactories.Details
{
    public class CashgameDetailsPageModelFactory : ICashgameDetailsPageModelFactory
    {
        private readonly IPagePropertiesFactory _pagePropertiesFactory;
        private readonly ICashgameDetailsTableModelFactory _cashgameDetailsTableModelFactory;
        private readonly IUrlProvider _urlProvider;
        private readonly IGlobalization _globalization;

        public CashgameDetailsPageModelFactory(
            IPagePropertiesFactory pagePropertiesFactory, 
            ICashgameDetailsTableModelFactory cashgameDetailsTableModelFactory,
            IUrlProvider urlProvider,
            IGlobalization globalization)
        {
            _pagePropertiesFactory = pagePropertiesFactory;
            _cashgameDetailsTableModelFactory = cashgameDetailsTableModelFactory;
            _urlProvider = urlProvider;
            _globalization = globalization;
        }

        public CashgameDetailsPageModel Create(User user, Homegame homegame, Cashgame cashgame, Player player, bool isManager)
        {
            var dateStr = cashgame.StartTime.HasValue ? _globalization.FormatShortDate(cashgame.StartTime.Value, true) : string.Empty;
            var showStartTime = cashgame.Status >= GameStatus.Running && cashgame.StartTime.HasValue;
            var showEndTime = cashgame.Status >= GameStatus.Finished && cashgame.EndTime != null;
            
            var model = new CashgameDetailsPageModel
                {
                    BrowserTitle = "Cashgame",
                    PageProperties = _pagePropertiesFactory.Create(user, homegame),
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
                    EditUrl = _urlProvider.GetCashgameEditUrl(homegame, cashgame),
                    CheckpointsUrl = _urlProvider.GetCashgameActionUrl(homegame, cashgame, player),
                    ChartDataUrl = _urlProvider.GetCashgameDetailsChartJsonUrl(homegame, cashgame)
                };

            return model;
        }
    }
}