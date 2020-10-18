namespace PokerBunch.Common.Urls.SiteUrls
{
    public class ResetPasswordUrl : SiteUrl
    {
        protected override string Input => Route;
        public const string Route = "user/resetpassword";
    }
}