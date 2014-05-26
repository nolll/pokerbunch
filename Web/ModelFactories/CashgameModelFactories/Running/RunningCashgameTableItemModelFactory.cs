using System;
using Application.Services;
using Core.Entities;
using Web.Models.CashgameModels.Running;

namespace Web.ModelFactories.CashgameModelFactories.Running
{
    public class RunningCashgameTableItemModelFactory : IRunningCashgameTableItemModelFactory
    {
        private readonly IUrlProvider _urlProvider;
        private readonly ITimeProvider _timeProvider;
        private readonly IGlobalization _globalization;

        public RunningCashgameTableItemModelFactory(
            IUrlProvider urlProvider,
            ITimeProvider timeProvider,
            IGlobalization globalization)
        {
            _urlProvider = urlProvider;
            _timeProvider = timeProvider;
            _globalization = globalization;
        }

        public RunningCashgameTableItemModel Create(Homegame homegame, Cashgame cashgame, Player player, CashgameResult result, bool isManager)
        {
            return new RunningCashgameTableItemModel
                {
                    Name = player.DisplayName,
                    PlayerUrl = _urlProvider.GetCashgameActionUrl(homegame.Slug, cashgame.DateString, player.Id),
                    BuyinUrl = _urlProvider.GetCashgameBuyinUrl(homegame.Slug, player.Id),
                    ReportUrl = _urlProvider.GetCashgameReportUrl(homegame.Slug, player.Id),
                    CashoutUrl = _urlProvider.GetCashgameCashoutUrl(homegame.Slug, player.Id),
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