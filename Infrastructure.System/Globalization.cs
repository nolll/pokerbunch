using System;
using System.Collections.Generic;
using System.Globalization;
using Application.Services;
using Application.UseCases.CashgameFacts;
using Core.Classes;

namespace Infrastructure.System{

	public class Globalization : IGlobalization
	{
		public string FormatNumber(int number)
		{
            var culture = CultureInfo.CreateSpecificCulture("sv-SE");
            return number.ToString("N0", culture);
		}

        public string FormatCurrency(Currency currency, int amount)
        {
            var numberFormatted = FormatNumber(amount);
            var amountFormatted = currency.Layout.Replace("{AMOUNT}", numberFormatted);
            return amountFormatted.Replace("{SYMBOL}", currency.Symbol);
        }

        public string FormatCurrency(Money money)
        {
            return FormatCurrency(money.Currency, money.Amount);
        }

	    public string FormatWinrate(Money winrate)
	    {
	        return FormatWinrate(winrate.Currency, winrate.Amount);
	    }

	    public string FormatWinrate(Currency currency, int winrate)
		{
            return FormatCurrency(currency, winrate) + "/h";
		}

        public string FormatResult(Currency currency, int result)
        {
            var currencyValue = FormatCurrency(currency, result);
            if (result > 0)
            {
                return "+" + currencyValue;
            }
            return currencyValue;
        }

        public string FormatResult(Money money)
        {
            return FormatResult(money.Currency, money.Amount);
        }

        public string FormatDuration(int minutes)
        {
            var h = (int)Math.Floor((double)minutes / 60);
            var m = minutes % 60;
            if (h > 0 && m > 0)
            {
                return h + "h " + m + "m";
            }
            if (h > 0)
            {
                return h + "h";
            }
            return m + "m";
        }

        public string FormatDuration(TimeSpan timeSpan)
        {
            return FormatDuration(Convert.ToInt32(Math.Round(timeSpan.TotalMinutes)));
        }

        public string FormatTimespan(TimeSpan timeSpan)
        {
            var minutes = (int)Math.Round(timeSpan.TotalMinutes);
            if (minutes == 0)
            {
                return "now";
            }
            if (minutes == 1)
            {
                return "1 minute";
            }
            return minutes + " minutes";
        }
        
        public string FormatShortDate(DateTime date, bool includeYear = false)
		{
            return date.ToString(GetShortDateFormat(includeYear), CultureInfo.InvariantCulture);
		}

		public string FormatShortDateTime(DateTime date, bool includeYear = false)
		{
            return date.ToString(GetShortDateTimeFormat(includeYear), CultureInfo.InvariantCulture);
		}

		public string FormatTime(DateTime date)
		{
            return date.ToString("HH:mm", CultureInfo.InvariantCulture);
		}

		public string FormatIsoDate(DateTime date)
		{
            return date.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
		}

		public string FormatIsoDateTime(DateTime date)
		{
            return date.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
		}

		public string FormatYear(DateTime date)
		{
            return date.ToString("yyyy", CultureInfo.InvariantCulture);
		}

		public IList<TimeZoneInfo> GetTimezones()
		{
            return TimeZoneInfo.GetSystemTimeZones();
		}

		public List<string> GetCurrencyLayouts()
		{
            return new List<string>
		        {
		            "{SYMBOL} {AMOUNT}",
		            "{SYMBOL}{AMOUNT}",
		            "{AMOUNT}{SYMBOL}",
		            "{AMOUNT} {SYMBOL}"
		        };
		}

        private string GetShortDateFormat(bool includeYear = false)
        {
            return includeYear ? "MMM d yyyy" : "MMM d";
        }

        private string GetShortDateTimeFormat(bool includeYear = false)
        {
            return includeYear ? "MMM d yyyy HH:mm" : "MMM d HH:mm";
        }

    }
}