using Core.Classes;
using Web.Routing;

namespace Web.Models.Url{

	public class HomegameDetailsUrlModel : HomegameUrlModel{

		public HomegameDetailsUrlModel(Homegame homegame) : base(RouteFormats.HomegameDetails, homegame){}

	}

}