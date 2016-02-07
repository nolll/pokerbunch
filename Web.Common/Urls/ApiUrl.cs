namespace Web.Common.Urls
{
    public abstract class ApiUrl : Url
    {
        protected ApiUrl(string url)
            : base(url)
        {
        }

        protected override string GetDomainName()
        {
            return CommonSettings.Instance.ApiHost;
        }
    }
}