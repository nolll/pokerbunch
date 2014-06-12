namespace Web.Models.UrlModels
{
    public abstract class CashgameUrlModel : UrlModel
    {
        protected CashgameUrlModel(string format, string slug, string dateStr)
            : base(string.Format(format, slug, dateStr))
        {
        }
    }
}