using System;
using System.Collections.Generic;
using System.Globalization;
using Core.Classes;

namespace Infrastructure.System{

	public static class Globalization{

		public static string FormatNumber(int number)
		{
		    var culture = CultureInfo.CreateSpecificCulture("sv-SE");
		    return number.ToString("N0", culture);
		}

		public static string FormatCurrency(CurrencySettings currency, int amount){
            var numberFormatted = FormatNumber(amount);
			var amountFormatted = currency.Layout.Replace("{AMOUNT}", numberFormatted);
            return amountFormatted.Replace("{SYMBOL}", currency.Symbol);
		}

		public static string FormatWinrate(CurrencySettings currency, int winrate)
		{
		    return FormatCurrency(currency, winrate) + "/h";
		}

		public static string FormatResult(CurrencySettings currencySettings, int result){
            var currency = FormatCurrency(currencySettings, result);
			if(result > 0){
				return "+" + currency;
			}
			return currency;
		}

		public static string FormatDuration(int minutes){
			var h = (int)Math.Floor((double)minutes / 60);
			var m = minutes % 60;
			if(h > 0 && m > 0){
				return h + "h " + m + "m";
			}
            if (h > 0){
				return h + "h";
			}
			return m + "m";
		}

		public static string FormatTimespan(TimeSpan timespan){
			if(timespan.Minutes == 0){
				return "now";
			}
			if(timespan.Minutes == 1){
				return "1 minute";
			}
			return timespan.Minutes + " minutes";
		}

		public static string FormatShortDate(DateTime date, bool includeYear = false){
			return date.ToString(GetShortDateFormat(includeYear), CultureInfo.InvariantCulture);
		}

		public static string FormatShortDateTime(DateTime date, bool includeYear = false){
			return date.ToString(GetShortDateTimeFormat(includeYear), CultureInfo.InvariantCulture);
		}

		public static string FormatTime(DateTime date){
            return date.ToString("HH:mm", CultureInfo.InvariantCulture);
		}

		public static string FormatIsoDate(DateTime date){
            return date.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
		}

		public static string FormatIsoDateTime(DateTime date){
            return date.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
            return "";//$date.format("Y-m-d H:i:s");
		}

		public static string FormatYear(DateTime date){
            return date.ToString("yyyy", CultureInfo.InvariantCulture);
		}

		public static List<string> GetTimezoneNames(){
			return new List<string>();//array_values(array_diff(DateTimeZone::listIdentifiers(), self::getInvalidTimezoneNames()));
		}

		public static string GetDefaultTimezoneName(){
			return "America/New_York";
		}

		public static string GetDefaultCurrency(){
			return "$";
		}

		public static string GetDefaultCurrencyLayout(){
			return "{SYMBOL}{AMOUNT}";
		}

		public static List<string> GetCurrencyLayouts()
		{
		    return new List<string>
		        {
		            "{SYMBOL} {AMOUNT}",
		            "{SYMBOL}{AMOUNT}",
		            "{AMOUNT}{SYMBOL}",
		            "{AMOUNT} {SYMBOL}"
		        };
		}

		private static string GetShortDateFormat(bool includeYear = false)
		{
		    return includeYear ? "MMM d yyyy" : "MMM d";
		}

	    private static string GetShortDateTimeFormat(bool includeYear = false)
	    {
	        return includeYear ? "MMM d yyyy HH:mm" : "MMM d HH:mm";
	    }

	    private static List<string> GetInvalidTimezoneNames(){
			return new List<string>{
						"Brazil/Acre","Brazil/DeNoronha","Brazil/East","Brazil/West","Canada/Atlantic","Canada/Central",
						"Canada/East-Saskatchewan","Canada/Eastern","Canada/Mountain","Canada/Newfoundland","Canada/Pacific",
						"Canada/Saskatchewan","Canada/Yukon","CET","Chile/Continental","Chile/EasterIsland","CST6CDT","Cuba",
						"EET","Egypt","Eire","EST","EST5EDT","Etc/GMT","Etc/GMT+0","Etc/GMT+1","Etc/GMT+10","Etc/GMT+11",
						"Etc/GMT+12","Etc/GMT+2","Etc/GMT+3","Etc/GMT+4","Etc/GMT+5","Etc/GMT+6","Etc/GMT+7","Etc/GMT+8",
						"Etc/GMT+9","Etc/GMT-0","Etc/GMT-1","Etc/GMT-10","Etc/GMT-11","Etc/GMT-12","Etc/GMT-13","Etc/GMT-14",
						"Etc/GMT-2","Etc/GMT-3","Etc/GMT-4","Etc/GMT-5","Etc/GMT-6","Etc/GMT-7","Etc/GMT-8","Etc/GMT-9",
						"Etc/GMT0","Etc/Greenwich","Etc/UCT","Etc/Universal","Etc/UTC","Etc/Zulu","Factory","GB","GB-Eire",
						"GMT","GMT+0","GMT-0","GMT0","Greenwich","Hongkong","HST","Iceland","Iran","Israel","Jamaica","Japan",
						"Kwajalein","Libya","MET","Mexico/BajaNorte","Mexico/BajaSur","Mexico/General","MST","MST7MDT","Navajo",
						"NZ","NZ-CHAT","Poland","Portugal","PRC","PST8PDT","ROC","ROK","Singapore","Turkey","UCT","Universal",
						"US/Alaska","US/Aleutian","US/Arizona","US/Central","US/East-Indiana","US/Eastern","US/Hawaii",
						"US/Indiana-Starke","US/Michigan","US/Mountain","US/Pacific","US/Pacific-New","US/Samoa","W-SU","WET","Zulu"
			};
		}
	}
}