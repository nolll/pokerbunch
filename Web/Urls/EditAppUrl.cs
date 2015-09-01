namespace Web.Urls
{
    public class EditAppUrl : IdUrl
    {
        public EditAppUrl(int appId)
            : base(Routes.AppEdit, appId)
        {
        }
    }
}