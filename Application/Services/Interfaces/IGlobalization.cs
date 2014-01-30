using System;
using System.Collections.Generic;
using Core.Classes;

namespace Application.Services.Interfaces
{
    public interface IGlobalization
    {
        string FormatCurrency(CurrencySettings currency, int amount);
        string FormatWinrate(CurrencySettings currency, int winrate);
        string FormatResult(CurrencySettings currency, int result);
        string FormatDuration(int minutes);
        string FormatTimespan(TimeSpan timespan);
        string FormatShortDate(DateTime date, bool includeYear = false);
        string FormatTime(DateTime date);
        string FormatIsoDate(DateTime date);
        string FormatIsoDateTime(DateTime date);
        IList<TimeZoneInfo> GetTimezones();
        List<string> GetCurrencyLayouts();
    }
}