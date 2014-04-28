using System;
using System.Collections.Generic;
using Application.UseCases.CashgameFacts;
using Core.Classes;

namespace Application.Services
{
    public interface IGlobalization
    {
        string FormatCurrency(CurrencySettings currency, int amount);
        string FormatCurrency(Money money);
        string FormatWinrate(Money winrate);
        string FormatWinrate(CurrencySettings currency, int winrate);
        string FormatResult(CurrencySettings currency, int result);
        string FormatResult(Money money);
        string FormatDuration(int minutes);
        string FormatDuration(TimeSpan timeSpan);
        string FormatTimespan(TimeSpan timeSpan);
        string FormatShortDate(DateTime date, bool includeYear = false);
        string FormatTime(DateTime date);
        string FormatIsoDate(DateTime date);
        string FormatIsoDateTime(DateTime date);
        IList<TimeZoneInfo> GetTimezones();
        List<string> GetCurrencyLayouts();
    }
}