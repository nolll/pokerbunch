using Core.Classes;
using Web.Routing;

namespace Web.Models.UrlModels{

	public class HomegameEditUrlModel : HomegameUrlModel{

		public HomegameEditUrlModel(Homegame homegame) : base(RouteFormats.HomegameEdit, homegame)
        {
		}

	}

}