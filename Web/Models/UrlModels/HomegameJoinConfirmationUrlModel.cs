using Core.Classes;
using Web.Routing;

namespace Web.Models.UrlModels{

	public class HomegameJoinConfirmationUrlModel : HomegameUrlModel{

		public HomegameJoinConfirmationUrlModel(Homegame homegame)
			: base(RouteFormats.HomegameJoinConfirmation, homegame)
        {
		}

	}

}