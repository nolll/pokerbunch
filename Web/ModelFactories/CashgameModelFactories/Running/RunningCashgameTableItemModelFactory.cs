using System;
using Core.Classes;
using Core.Services;
using Infrastructure.System;
using Web.Models.CashgameModels.Running;
using Web.Services;

namespace Web.ModelFactories.CashgameModelFactories.Running
{
    public class RunningCashgameTableItemModelFactory : IRunningCashgameTableItemModelFactory
    {
        private readonly IUrlProvider _urlProvider;
        private readonly ITimeProvider _timeProvider;
        private readonly IResultFormatter _resultFormatter;

        public RunningCashgameTableItemModelFactory(
            IUrlProvider urlProvider,
            ITimeProvider timeProvider,
            IResultFormatter resultFormatter)
        {
            _urlProvider = urlProvider;
            _timeProvider = timeProvider;
            _resultFormatter = resultFormatter;
        }

        public RunningCashgameTableItemModel Create(Homegame homegame, Cashgame cashgame, CashgameResult result, bool isManager)
        {
            return new RunningCashgameTableItemModel
                {
                    Name = result.Player != null ? result.Player.DisplayName : null,
                    PlayerUrl = result.Player != null ? _urlProvider.GetCashgameActionUrl(homegame, cashgame, result.Player) : null,
                    BuyinUrl = result.Player != null && isManager ? _urlProvider.GetCashgameBuyinUrl(homegame, result.Player) : null,
                    ReportUrl = result.Player != null && isManager ? _urlProvider.GetCashgameReportUrl(homegame, result.Player) : null,
                    CashoutUrl = result.Player != null && isManager ? _urlProvider.GetCashgameCashoutUrl(homegame, result.Player) : null,
                    Buyin = Globalization.FormatCurrency(homegame.Currency, result.Buyin),
                    Stack = Globalization.FormatCurrency(homegame.Currency, result.Stack),
                    Winnings = Globalization.FormatResult(homegame.Currency, result.Winnings),
                    Time = GetTime(result.LastReportTime),
                    WinningsClass = _resultFormatter.GetWinningsCssClass(result.Winnings),
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