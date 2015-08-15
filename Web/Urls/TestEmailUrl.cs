using Core.Urls;

namespace Web.Urls
{
    public class TestEmailUrl : Url
    {
        public TestEmailUrl()
            : base("-/admin/sendemail")
        {
        }
    }
}