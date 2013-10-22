using System;
using System.Collections.Generic;
using Core.Classes;

namespace Infrastructure.System{

	public class Globalization : IGlobalization
	{

		public string FormatNumber(int number)
		{
		    return StaticGlobalization.FormatNumber(number);
		}

		public string FormatCurrency(CurrencySettings currency, int amount)
		{
		    return StaticGlobalization.FormatCurrency(currency, amount);
		}

		public string FormatWinrate(CurrencySettings currency, int winrate)
		{
		    return StaticGlobalization.FormatWinrate(currency, winrate);
		}

		public string FormatResult(CurrencySettings currency, int result)
		{
		    return StaticGlobalization.FormatResult(currency, result);
		}

		public string FormatDuration(int minutes)
		{
		    return StaticGlobalization.FormatDuration(minutes);
		}

		public string FormatTimespan(TimeSpan timespan)
		{
		    return StaticGlobalization.FormatTimespan(timespan);
		}

		public string FormatShortDate(DateTime date, bool includeYear = false)
		{
		    return StaticGlobalization.FormatShortDate(date, includeYear);
		}

		public string FormatShortDateTime(DateTime date, bool includeYear = false)
		{
		    return StaticGlobalization.FormatShortDateTime(date, includeYear);
		}

		public string FormatTime(DateTime date)
		{
		    return StaticGlobalization.FormatTime(date);
		}

		public string FormatIsoDate(DateTime date)
		{
		    return StaticGlobalization.FormatIsoDate(date);
		}

		public string FormatIsoDateTime(DateTime date)
		{
		    return StaticGlobalization.FormatIsoDateTime(date);
		}

		public string FormatYear(DateTime date)
		{
		    return StaticGlobalization.FormatYear(date);
		}

		public IList<TimeZoneInfo> GetTimezones()
		{
		    return StaticGlobalization.GetTimezones();
		}

		public string GetDefaultTimezoneName()
		{
		    return StaticGlobalization.GetDefaultTimezoneName();
		}

		public string GetDefaultCurrency(){
			return StaticGlobalization.GetDefaultCurrency();
		}

		public string GetDefaultCurrencyLayout(){
			return StaticGlobalization.GetDefaultCurrencyLayout();
		}

		public List<string> GetCurrencyLayouts()
		{
		    return StaticGlobalization.GetCurrencyLayouts();
		}

	}
}