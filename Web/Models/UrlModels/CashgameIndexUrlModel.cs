using Web.Routing;

namespace Web.Models.UrlModels
{
    public class CashgameIndexUrlModel : HomegameUrlModel
    {
        public CashgameIndexUrlModel(string slug)
            : base(RouteFormats.CashgameIndex, slug)
        {
        }
    }
}