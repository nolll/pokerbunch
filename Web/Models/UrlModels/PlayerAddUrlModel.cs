using Core.Classes;
using Web.Routing;

namespace Web.Models.UrlModels{
    public class PlayerAddUrlModel : HomegameUrlModel{

		public PlayerAddUrlModel(Homegame homegame)
            : base(RouteFormats.PlayerAdd, homegame)
        {
		}

	}

}