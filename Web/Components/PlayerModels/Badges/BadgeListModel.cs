using System.Collections.Generic;
using Core.UseCases.PlayerBadges;
using Web.Extensions;

namespace Web.Components.PlayerModels.Badges
{
	public class BadgeListModel : Component
    {
        public IList<BadgeModel> NumberOfGamesBadges { get; private set; }

	    public BadgeListModel(PlayerBadgesResult badgesResult)
	    {
	        NumberOfGamesBadges = new List<BadgeModel>
	        {
	            new BadgeModel("Played one game", badgesResult.PlayedOneGame),
	            new BadgeModel("Played ten games", badgesResult.PlayedTenGames),
	            new BadgeModel("Played 50 games", badgesResult.Played50Games),
	            new BadgeModel("Played 100 games", badgesResult.Played100Games),
	            new BadgeModel("Played 200 games", badgesResult.Played200Games),
	            new BadgeModel("Played 500 games", badgesResult.Played500Games)
	        };
	    }
    }
}