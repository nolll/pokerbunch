using Core.Classes;
using Web.Routing;

namespace Web.Models.Url{

	public class HomegameEditUrlModel : HomegameUrlModel{

		public HomegameEditUrlModel(Homegame homegame) : base(RouteFormats.HomegameEdit, homegame)
        {
		}

	}

}