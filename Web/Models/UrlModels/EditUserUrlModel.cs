using Web.Routing;

namespace Web.Models.UrlModels
{
    public class EditUserUrlModel : UserUrlModel
    {
        public EditUserUrlModel(string userName)
            : base(RouteFormats.UserEdit, userName)
        {
        }
    }
}