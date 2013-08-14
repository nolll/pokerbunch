using Core.Classes;
using Web.Models;
using Web.Routing;

namespace app{

	public class HomegameDetailsUrlModel : HomegameUrlModel{

		public HomegameDetailsUrlModel(Homegame homegame) : base(RouteFormats.HomegameDetails, homegame){}

	}

}