namespace Core.Urls
{
    public class TestEmailUrl : Url
    {
        public TestEmailUrl()
            : base("-/admin/sendemail")
        {
        }
    }

    public class ClearCacheUrl : Url
    {
        public ClearCacheUrl()
            : base("-/admin/clearcache")
        {
        }
    }
}