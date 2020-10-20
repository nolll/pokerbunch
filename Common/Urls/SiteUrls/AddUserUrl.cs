namespace PokerBunch.Common.Urls.SiteUrls
{
    public class AddUserUrl : SiteUrl
    {
        private const string Route = "user/add";
        protected override string Input => Route;
    }
}