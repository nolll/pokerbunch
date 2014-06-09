using Web.Services;

namespace Web.Models.UrlModels
{
    public abstract class CashgamePlayerUrlModel : UrlModel
    {
        protected CashgamePlayerUrlModel(string format, string slug, string dateStr, int playerId)
            : base(UrlProvider.FormatCashgamePlayerUrl(format, slug, dateStr, playerId))
        {
        }
    }
}