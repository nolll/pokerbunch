using Core.Classes;
using Web.Routing;

namespace Web.Models.UrlModels{

	class PlayerAddUrlModel : HomegameUrlModel{

		public PlayerAddUrlModel(Homegame homegame)
            : base(RouteFormats.PlayerAdd, homegame)
        {
		}

	}

}