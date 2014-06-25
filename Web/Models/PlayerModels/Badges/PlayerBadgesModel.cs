using Application.UseCases.PlayerBadges;

namespace Web.Models.PlayerModels.Badges
{
	public class PlayerBadgesModel
    {
	    public BadgeModel PlayedOneGame { get; private set; }
        public BadgeModel PlayedTenGames { get; private set; }
        public BadgeModel Played50Games { get; private set; }
        public BadgeModel Played100Games { get; private set; }
        public BadgeModel Played200Games { get; private set; }
        public BadgeModel Played500Games { get; private set; }

	    public PlayerBadgesModel(PlayerBadgesResult badgesResult)
	    {
	        PlayedOneGame = new BadgeModel("Played one game", badgesResult.PlayedOneGame);
            PlayedTenGames = new BadgeModel("Played ten games", badgesResult.PlayedTenGames);
            Played50Games = new BadgeModel("Played 50 games", badgesResult.Played50Games);
            Played100Games = new BadgeModel("Played 100 games", badgesResult.Played100Games);
            Played200Games = new BadgeModel("Played 200 games", badgesResult.Played200Games);
            Played500Games = new BadgeModel("Played 500 games", badgesResult.Played500Games);
	    }
    }
}