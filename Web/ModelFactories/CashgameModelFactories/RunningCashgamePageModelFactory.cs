using System.Collections.Generic;
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
            var model = new RunningCashgamePageModel();

            model.BrowserTitle = "Running Cashgame";
            model.PageProperties = _pagePropertiesFactory.Create(user, homegame, runningGame);
            model.Location = cashgame.Location;

            if (cashgame.IsStarted)
            {
                model.ShowStartTime = true;
                model.StartTime = Globalization.FormatTime(cashgame.StartTime.Value);
            }
            else
            {
                model.ShowStartTime = false;
            }

            model.BuyinUrl = _urlProvider.GetCashgameBuyinUrl(homegame, player);
            model.ReportUrl = new CashgameReportUrlModel(homegame, player);
            model.CashoutUrl = new CashgameCashoutUrlModel(homegame, player);
            model.EndGameUrl = new CashgameEndUrlModel(homegame);

            var canBeEnded = CanBeEnded(cashgame);
            var canReport = !canBeEnded;
            var isInGame = cashgame.IsInGame(player);

            model.BuyinButtonEnabled = canReport;
            model.ReportButtonEnabled = canReport && isInGame;
            model.CashoutButtonEnabled = isInGame;
            model.EndGameButtonEnabled = canBeEnded;

            if (cashgame.IsStarted)
            {
                model.RunningCashgameTableModel = _runningCashgameTableModelFactory.Create(homegame, cashgame, isManager);
                model.ShowTable = true;
            }
            else
            {
                model.ShowTable = false;
            }

            if (cashgame.IsStarted)
            {
                model.ChartDataUrl = new CashgameDetailsChartJsonUrlModel(homegame, cashgame);
                model.ShowChart = true;
            }
            else
            {
                model.ShowChart = false;
            }

            return model;
        }

        private bool CanBeEnded(Cashgame cashgame)
        {
            return cashgame.IsStarted && !cashgame.HasActivePlayers;
        }
    }
}