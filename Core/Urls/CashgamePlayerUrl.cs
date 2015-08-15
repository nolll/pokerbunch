using System.Globalization;

namespace Core.Urls
{
    public abstract class CashgamePlayerUrl : Url
    {
        protected CashgamePlayerUrl(string format, string slug, string dateStr, int playerId)
            : base(format.Replace("{slug}", slug).Replace("{dateStr}", dateStr).Replace("{playerId}", playerId.ToString(CultureInfo.InvariantCulture)))
        {
        }
    }
}