using System.Globalization;

namespace Web{

	public static class Util{

		public static string FormatWinnings(int winnings){
			if(winnings > 0){
				return "+"  + winnings;
			}
			return winnings.ToString(CultureInfo.InvariantCulture);
		}

		public static string GetWinningsCssClass(int winnings){
			if(winnings > 0){
				return "pos-result";
			}
            if (winnings < 0) {
				return "neg-result";
			}
			return string.Empty;
		}

	}

}