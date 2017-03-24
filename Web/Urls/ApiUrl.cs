namespace Web.Urls
{
    public abstract class ApiUrl : Url
    {
        public override UrlType Type => UrlType.Api;
    }
}