using System;
using System.Collections.Generic;
using Core.Classes;

namespace Infrastructure.System
{
    public interface IGlobalization
    {
        string FormatNumber(int number);
        string FormatCurrency(CurrencySettings currency, int amount);
        string FormatWinrate(CurrencySettings currency, int winrate);
        string FormatResult(CurrencySettings currency, int result);
        string FormatDuration(int minutes);
        string FormatTimespan(TimeSpan timespan);
        string FormatShortDate(DateTime date, bool includeYear = false);
        string FormatShortDateTime(DateTime date, bool includeYear = false);
        string FormatTime(DateTime date);
        string FormatIsoDate(DateTime date);
        string FormatIsoDateTime(DateTime date);
        string FormatYear(DateTime date);
        IList<TimeZoneInfo> GetTimezones();
        string GetDefaultTimezoneName();
        string GetDefaultCurrency();
        string GetDefaultCurrencyLayout();
        List<string> GetCurrencyLayouts();
    }
}