namespace Web.Urls.SiteUrls
{
    public class DeleteCashgameUrl : IdUrl
    {
        public DeleteCashgameUrl(string id)
            : base(WebRoutes.Cashgame.Delete, id)
        {
        }
    }
}