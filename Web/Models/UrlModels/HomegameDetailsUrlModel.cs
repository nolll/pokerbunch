using Core.Classes;
using Web.Routing;

namespace Web.Models.UrlModels{

	public class HomegameDetailsUrlModel : HomegameUrlModel{

		public HomegameDetailsUrlModel(Homegame homegame) : base(RouteFormats.HomegameDetails, homegame){}

	}

}