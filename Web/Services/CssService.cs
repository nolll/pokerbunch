using Core.Entities;

namespace Web.Services
{
    public class CssService
    {
        public static string GetWinningsCssClass(Money winnings)
        {
            return GetWinningsCssClass(winnings.Amount);
        }

        private static string GetWinningsCssClass(int winnings)
        {
            if (winnings == 0)
                return string.Empty;
            return winnings > 0 ? "pos-result" : "neg-result";
        }
    }
}