using Web.Routing;

namespace Web.Models.UrlModels
{
    public class AddCashgameUrlModel : HomegameUrlModel
    {
        public AddCashgameUrlModel(string slug)
            : base(RouteFormats.CashgameAdd, slug)
        {
        }
    }
}