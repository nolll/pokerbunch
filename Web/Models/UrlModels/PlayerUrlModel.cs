using Web.Services;

namespace Web.Models.UrlModels
{
    public abstract class PlayerUrlModel : UrlModel
    {
        protected PlayerUrlModel(string format, string slug, int playerId)
            : base(UrlProvider.FormatPlayer(format, slug, playerId))
        {
        }
    }
}