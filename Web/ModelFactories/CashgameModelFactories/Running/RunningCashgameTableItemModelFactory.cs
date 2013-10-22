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
        private readonly IGlobalization _globalization;

        public RunningCashgameTableItemModelFactory(
            IUrlProvider urlProvider,
            ITimeProvider timeProvider,
            IResultFormatter resultFormatter,
            IGlobalization globalization)
        {
            _urlProvider = urlProvider;
            _timeProvider = timeProvider;
            _resultFormatter = resultFormatter;
            _globalization = globalization;
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
                    Buyin = _globalization.FormatCurrency(homegame.Currency, result.Buyin),
                    Stack = _globalization.FormatCurrency(homegame.Currency, result.Stack),
                    Winnings = _globalization.FormatResult(homegame.Currency, result.Winnings),
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
                return _globalization.FormatTimespan(timespan);
            }
            return null;
        }
    }
}