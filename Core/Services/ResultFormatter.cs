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
            var str = winnings.ToString();
            if (winnings.Amount > 0)
                return "+" + str;
            return str;
        }

        public static string GetWinningsCssClass(Money winnings)
        {
            return GetWinningsCssClass(winnings.Amount);
        }

		public static string GetWinningsCssClass(int winnings)
        {
            if(winnings == 0)
                return string.Empty;
            return winnings > 0 ? "pos-result" : "neg-result";
		}
	}
}