using System;
using Application.Services;
using Application.Urls;
using Core.Entities;
using Web.Models.CashgameModels.Running;

namespace Web.ModelFactories.CashgameModelFactories.Running
{
    public class RunningCashgameTableItemModelFactory : IRunningCashgameTableItemModelFactory
    {
        private readonly ITimeProvider _timeProvider;

        public RunningCashgameTableItemModelFactory(ITimeProvider timeProvider)
        {
            _timeProvider = timeProvider;
        }

        public RunningCashgameTableItemModel Create(Bunch bunch, Cashgame cashgame, Player player, CashgameResult result, bool isManager)
        {
            return new RunningCashgameTableItemModel
                {
                    Name = player.DisplayName,
                    PlayerUrl = new CashgameActionUrl(bunch.Slug, cashgame.DateString, player.Id),
                    BuyinUrl = new CashgameBuyinUrl(bunch.Slug, player.Id),
                    ReportUrl = new CashgameReportUrl(bunch.Slug, player.Id),
                    CashoutUrl = new CashgameCashoutUrl(bunch.Slug, player.Id),
                    Buyin = Globalization.FormatCurrency(bunch.Currency, result.Buyin),
                    Stack = Globalization.FormatCurrency(bunch.Currency, result.Stack),
                    Winnings = Globalization.FormatResult(bunch.Currency, result.Winnings),
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
                return Globalization.FormatTimespan(timespan);
            }
            return null;
        }
    }
}