using Web.Services;

namespace Web.Models.UrlModels
{
    public abstract class HomegameUrlModel : UrlModel
    {
        protected HomegameUrlModel(string format, string slug)
            : base(UrlProvider.FormatHomegame(format, slug))
        {
        }
    }
}