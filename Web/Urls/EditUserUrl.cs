namespace Core.Urls
{
    public class EditUserUrl : UserUrl
    {
        public EditUserUrl(string userName)
            : base(Routes.UserEdit, userName)
        {
        }
    }
}