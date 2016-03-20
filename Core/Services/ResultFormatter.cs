using System.Globalization;
using Core.Entities;

namespace Core.Services
{
	public static class ResultFormatter
	{
		public static string FormatWinnings(int winnings)
        {
			if(winnings > 0)
				return "+"  + winnings;
			return winnings.ToString(CultureInfo.InvariantCulture);
		}

        public static string FormatWinnings(Money winnings)
        {
            var str = winnings.String;
            if (winnings.Amount > 0)
                return "+" + str;
            return str;
        }

	    public static string FormatMoney(Money money)
	    {
	        return money.String;
	    }

        public static string FormatWinRate(Money winRate)
        {
            return FormatWinnings(winRate) + "/h";
        }
    }
}