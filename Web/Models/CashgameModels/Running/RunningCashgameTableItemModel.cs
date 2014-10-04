using System;
using Application.Services;
using Application.Urls;
using Core.Entities;

namespace Web.Models.CashgameModels.Running
{
    public class RunningCashgameTableItemModel
    {
        public string Name { get; private set; }
        public Url PlayerUrl { get; private set; }
        public string Buyin { get; private set; }
        public string Stack { get; private set; }
        public string Winnings { get; private set; }
        public string Time { get; private set; }
        public string WinningsClass { get; private set; }
        public bool ManagerButtonsEnabled { get; private set; }
        public Url BuyinUrl { get; private set; }
        public Url ReportUrl { get; private set; }
        public Url CashoutUrl { get; private set; }
        public bool HasCashedOut { get; private set; }

        public RunningCashgameTableItemModel(Bunch bunch, Cashgame cashgame, Player player, CashgameResult result, bool isManager, DateTime now)
        {
            Name = player.DisplayName;
            PlayerUrl = new CashgameActionUrl(bunch.Slug, cashgame.DateString, player.Id);
            BuyinUrl = new CashgameBuyinUrl(bunch.Slug, player.Id);
            ReportUrl = new CashgameReportUrl(bunch.Slug, player.Id);
            CashoutUrl = new CashgameCashoutUrl(bunch.Slug, player.Id);
            Buyin = Globalization.FormatCurrency(bunch.Currency, result.Buyin);
            Stack = Globalization.FormatCurrency(bunch.Currency, result.Stack);
            Winnings = Globalization.FormatResult(bunch.Currency, result.Winnings);
            Time = GetTime(result.LastReportTime, now);
            WinningsClass = ResultFormatter.GetWinningsCssClass(result.Winnings);
            HasCashedOut = result.CashoutTime != null;
            ManagerButtonsEnabled = isManager;
        }

        private string GetTime(DateTime? lastReportedTime, DateTime now)
        {
            if (!lastReportedTime.HasValue)
                return null;
            var timespan = now - lastReportedTime.Value;
            return Globalization.FormatTimespan(timespan);
        }
    }
}