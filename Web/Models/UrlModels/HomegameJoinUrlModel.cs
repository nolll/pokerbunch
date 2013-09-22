using Core.Classes;
using Web.Routing;

namespace Web.Models.UrlModels{

	public class HomegameJoinUrlModel : HomegameUrlModel{

		public HomegameJoinUrlModel(Homegame homegame)
            : base(RouteFormats.HomegameJoin, homegame)
        {
		}

	}

}