using Web.Models.UrlModels;
using Web.Routing;

namespace Web.Services
{
    public class AddCashgameUrlModel : HomegameUrlModel
    {
        public AddCashgameUrlModel(string slug)
            : base(RouteFormats.CashgameAdd, slug)
        {
        }
    }
}