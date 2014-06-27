using System;
using System.Collections.Generic;
using Core.Entities;

namespace Application.Services
{
    public class GlobalizationImpl : IGlobalization
    {
        public string FormatCurrency(Currency currency, int amount)
        {
            return Globalization.FormatCurrency(currency, amount);
        }

        public string FormatResult(Currency currency, int result)
        {
            return Globalization.FormatResult(currency, result);
        }

        public string FormatDuration(int minutes)
        {
            return Globalization.FormatDuration(minutes);
        }

        public string FormatTimespan(TimeSpan timeSpan)
        {
            return Globalization.FormatTimespan(timeSpan);
        }

        public string FormatShortDate(DateTime date, bool includeYear = false)
        {
            return Globalization.FormatShortDate(date, includeYear);
        }

        public string FormatTime(DateTime dateTime)
        {
            return Globalization.FormatTime(dateTime);
        }

        public string FormatIsoDate(DateTime date)
        {
            return Globalization.FormatIsoDate(date);
        }

        public IList<TimeZoneInfo> GetTimezones()
        {
            return Globalization.GetTimezones();
        }

        public IList<string> GetCurrencyLayouts()
        {
            return Globalization.GetCurrencyLayouts();
        }
    }
}