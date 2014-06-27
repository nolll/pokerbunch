using System;
using System.Collections.Generic;
using Core.Entities;

namespace Application.Services
{
    public interface IGlobalization
    {
        string FormatCurrency(Currency currency, int amount);
        string FormatResult(Currency currency, int result);
        string FormatDuration(int minutes);
        string FormatTimespan(TimeSpan timeSpan);
        string FormatShortDate(DateTime date, bool includeYear = false);
        string FormatTime(DateTime dateTime);
        string FormatIsoDate(DateTime date);
        IList<TimeZoneInfo> GetTimezones();
        IList<string> GetCurrencyLayouts();
    }
}