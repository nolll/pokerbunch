using Web.Services;

namespace Web.Models.UrlModels
{
    public abstract class UserUrlModel : UrlModel
    {
        protected UserUrlModel(string format, string userName)
            : base(UrlProvider.FormatUser(format, userName))
        {
        }
    }
}