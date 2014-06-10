using Web.Models.UrlModels;
using Web.Routing;

namespace Web.Services
{
    public class EditUserUrlModel : UserUrlModel
    {
        public EditUserUrlModel(string userName)
            : base(RouteFormats.UserEdit, userName)
        {
        }
    }
}