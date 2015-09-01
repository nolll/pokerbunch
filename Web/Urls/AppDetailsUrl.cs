namespace Web.Urls
{
    public class AppDetailsUrl : IdUrl
    {
        public AppDetailsUrl(int appId)
            : base(Routes.AppDetails, appId)
        {
        }
    }
}