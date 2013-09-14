using System.Collections.Generic;
using Core.Classes;
using Infrastructure.System;
using Web.ModelFactories.PageBaseModelFactories;
using Web.Models.CashgameModels.Running;
using Web.Models.PageBaseModels;
using Web.Models.UrlModels;

namespace Web.ModelFactories.CashgameModelFactories
{
    public class RunningCashgamePageModelFactory : IRunningCashgamePageModelFactory
    {
        private readonly IPagePropertiesFactory _pagePropertiesFactory;

        public RunningCashgamePageModelFactory(IPagePropertiesFactory pagePropertiesFactory)
        {
            _pagePropertiesFactory = pagePropertiesFactory;
        }

        public RunningCashgamePageModel Create(User user, Homegame homegame, Cashgame cashgame, Player player, List<int> years, bool isManager, ITimeProvider timer, Cashgame runningGame = null)
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

            model.BuyinUrl = new CashgameBuyinUrlModel(homegame, player);
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
                model.RunningCashgameTableModel = new RunningCashgameTableModel(homegame, cashgame, isManager, timer);
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