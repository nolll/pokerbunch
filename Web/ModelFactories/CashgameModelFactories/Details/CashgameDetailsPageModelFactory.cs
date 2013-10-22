﻿using System.Collections.Generic;
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

        public CashgameDetailsPageModelFactory(
            IPagePropertiesFactory pagePropertiesFactory, 
            ICashgameDetailsTableModelFactory cashgameDetailsTableModelFactory,
            IUrlProvider urlProvider)
        {
            _pagePropertiesFactory = pagePropertiesFactory;
            _cashgameDetailsTableModelFactory = cashgameDetailsTableModelFactory;
            _urlProvider = urlProvider;
        }

        public CashgameDetailsPageModel Create(User user, Homegame homegame, Cashgame cashgame, Player player, IList<int> years, bool isManager, Cashgame runningGame = null)
        {
            var dateStr = cashgame.StartTime.HasValue ? StaticGlobalization.FormatShortDate(cashgame.StartTime.Value, true) : string.Empty;
            var showStartTime = cashgame.Status >= GameStatus.Running && cashgame.StartTime.HasValue;
            var showEndTime = cashgame.Status >= GameStatus.Finished && cashgame.EndTime != null;
            
            var model = new CashgameDetailsPageModel
                {
                    BrowserTitle = "Cashgame",
                    PageProperties = _pagePropertiesFactory.Create(user, homegame, runningGame),
                    Heading = string.Format("Cashgame {0}", dateStr),
			        Location = cashgame.Location,
                    Duration = StaticGlobalization.FormatDuration(cashgame.Duration),
			        DurationEnabled = cashgame.Duration > 0,
                    ShowStartTime = showStartTime,
			        StartTime = showStartTime ? StaticGlobalization.FormatTime(cashgame.StartTime.Value) : null,
			        ShowEndTime = showEndTime,
			        EndTime = showEndTime ? StaticGlobalization.FormatTime(cashgame.EndTime.Value) : null,
                    Status = GameStatusName.GetName(cashgame.Status),
                    CashgameDetailsTableModel = _cashgameDetailsTableModelFactory.Create(homegame, cashgame),
                    EnableEdit = isManager,
                    EnableCheckpointsButton = cashgame.IsInGame(player),
                    EditUrl = _urlProvider.GetCashgameEditUrl(homegame, cashgame),
                    CheckpointsUrl = _urlProvider.GetCashgameActionUrl(homegame, cashgame, player),
                    ChartDataUrl = _urlProvider.GetCashgameDetailsChartJsonUrl(homegame, cashgame)
                };

            return model;
        }
    }
}