namespace Web.Models.UrlModels
{
    public abstract class CashgamePlayerUrlModel : UrlModel
    {
        protected CashgamePlayerUrlModel(string format, string slug, string dateStr, int playerId)
            : base(string.Format(format, slug, dateStr, playerId))
        {
        }
    }
}