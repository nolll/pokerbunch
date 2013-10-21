﻿using System.Collections.Generic;
using Core.Classes;
using Core.Services;
using Infrastructure.System;
using Web.ModelFactories.PageBaseModelFactories;
using Web.Models.CashgameModels.Running;
using Web.Models.UrlModels;

namespace Web.ModelFactories.CashgameModelFactories
{
    public class RunningCashgamePageModelFactory : IRunningCashgamePageModelFactory
    {
        private readonly IPagePropertiesFactory _pagePropertiesFactory;
        private readonly IRunningCashgameTableModelFactory _runningCashgameTableModelFactory;
        private readonly IUrlProvider _urlProvider;

        public RunningCashgamePageModelFactory(
            IPagePropertiesFactory pagePropertiesFactory,
            IRunningCashgameTableModelFactory runningCashgameTableModelFactory,
            IUrlProvider urlProvider)
        {
            _pagePropertiesFactory = pagePropertiesFactory;
            _runningCashgameTableModelFactory = runningCashgameTableModelFactory;
            _urlProvider = urlProvider;
        }

        public RunningCashgamePageModel Create(User user, Homegame homegame, Cashgame cashgame, Player player, IList<int> years, bool isManager, Cashgame runningGame = null)
        {
            var canBeEnded = CanBeEnded(cashgame);
            var canReport = !canBeEnded;
            var isInGame = cashgame.IsInGame(player);
            
            return new RunningCashgamePageModel
                {
                    BrowserTitle = "Running Cashgame",
                    PageProperties = _pagePropertiesFactory.Create(user, homegame, runningGame),
                    Location = cashgame.Location,
                    ShowStartTime = cashgame.IsStarted,
                    StartTime = cashgame.IsStarted && cashgame.StartTime.HasValue ? Globalization.FormatTime(cashgame.StartTime.Value) : null,
                    BuyinUrl = _urlProvider.GetCashgameBuyinUrl(homegame, player),
                    ReportUrl = _urlProvider.GetCashgameReportUrl(homegame, player),
                    CashoutUrl = _urlProvider.GetCashgameCashoutUrl(homegame, player),
                    EndGameUrl = _urlProvider.GetCashgameEndUrl(homegame),
                    BuyinButtonEnabled = canReport,
                    ReportButtonEnabled = canReport && isInGame,
                    CashoutButtonEnabled = isInGame,
                    EndGameButtonEnabled = canBeEnded,
                    ShowTable = cashgame.IsStarted,
                    RunningCashgameTableModel = cashgame.IsStarted ? _runningCashgameTableModelFactory.Create(homegame, cashgame, isManager) : null,
                    ShowChart = cashgame.IsStarted,
                    ChartDataUrl = cashgame.IsStarted ? _urlProvider.GetCashgameDetailsChartJsonUrl(homegame, cashgame) : null
                };
        }

        private bool CanBeEnded(Cashgame cashgame)
        {
            return cashgame.IsStarted && !cashgame.HasActivePlayers;
        }
    }
}