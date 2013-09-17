using Core.Classes;
using Web.Routing;

namespace Web.Models.UrlModels{

	class PlayerAddConfirmationUrlModel : HomegameUrlModel{

		public PlayerAddConfirmationUrlModel(Homegame homegame)
            : base(RouteFormats.PlayerAddConfirmation, homegame)
        {
		}

	}

}