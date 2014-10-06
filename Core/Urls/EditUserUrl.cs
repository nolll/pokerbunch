namespace Core.Urls
{
    public class EditUserUrl : UserUrl
    {
        public EditUserUrl(string userName)
            : base(RouteFormats.UserEdit, userName)
        {
        }
    }
}