using System.Globalization;
using Application.UseCases.CashgameFacts;
using Core.Classes;

namespace Application.Services
{
	public class ResultFormatter : IResultFormatter
	{
		public string FormatWinnings(int winnings)
        {
			if(winnings > 0){
				return "+"  + winnings;
			}
			return winnings.ToString(CultureInfo.InvariantCulture);
		}

        public string GetWinningsCssClass(Money winnings)
        {
            return GetWinningsCssClass(winnings.Amount);
        }

		public string GetWinningsCssClass(int winnings)
        {
			if(winnings > 0)
            {
				return "pos-result";
			}
            if (winnings < 0)
            {
				return "neg-result";
			}
			return string.Empty;
		}
	}
}