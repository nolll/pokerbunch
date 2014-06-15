using System;
using Application.Services;
using Application.Urls;
using Core.Entities;
using Web.Models.CashgameModels.Running;
using Web.Models.UrlModels;

namespace Web.ModelFactories.CashgameModelFactories.Running
{
    public class RunningCashgameTableItemModelFactory : IRunningCashgameTableItemModelFactory
    {
        private readonly ITimeProvider _timeProvider;
        private readonly IGlobalization _globalization;

        public RunningCashgameTableItemModelFactory(
            ITimeProvider timeProvider,
            IGlobalization globalization)
        {
            _timeProvider = timeProvider;
            _globalization = globalization;
        }

        public RunningCashgameTableItemModel Create(Homegame homegame, Cashgame cashgame, Player player, CashgameResult result, bool isManager)
        {
            return new RunningCashgameTableItemModel
                {
                    Name = player.DisplayName,
                    PlayerUrl = new CashgameActionUrl(homegame.Slug, cashgame.DateString, player.Id),
                    BuyinUrl = new CashgameBuyinUrl(homegame.Slug, player.Id),
                    ReportUrl = new CashgameReportUrl(homegame.Slug, player.Id),
                    CashoutUrl = new CashgameCashoutUrl(homegame.Slug, player.Id),
                    Buyin = _globalization.FormatCurrency(homegame.Currency, result.Buyin),
                    Stack = _globalization.FormatCurrency(homegame.Currency, result.Stack),
                    Winnings = _globalization.FormatResult(homegame.Currency, result.Winnings),
                    Time = GetTime(result.LastReportTime),
                    WinningsClass = ResultFormatter.GetWinningsCssClass(result.Winnings),
                    HasCashedOut = result.CashoutTime != null,
                    ManagerButtonsEnabled = isManager
                };
        }

        private string GetTime(DateTime? lastReportedTime)
        {
            if (lastReportedTime.HasValue)
            {
                var timespan = _timeProvider.GetTime() - lastReportedTime.Value;
                return _globalization.FormatTimespan(timespan);
            }
            return null;
        }
    }
}