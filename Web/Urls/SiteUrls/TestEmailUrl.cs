namespace Web.Urls.SiteUrls
{
    public class TestEmailUrl : SiteUrl
    {
        public TestEmailUrl()
            : base(WebRoutes.Admin.SendEmail)
        {
        }
    }
}