namespace Web.Models.PlayerModels.Badges
{
	public class PlayerBadgesModel
    {
	    public BadgeModel PlayedOneGame { get; set; }
        public BadgeModel PlayedTenGames { get; set; }
        public BadgeModel Played50Games { get; set; }
        public BadgeModel Played100Games { get; set; }
        public BadgeModel Played200Games { get; set; }
        public BadgeModel Played500Games { get; set; }

	    public PlayerBadgesModel(
            BadgeModel playedOneGame,
            BadgeModel playedTenGames,
            BadgeModel played50Games,
            BadgeModel played100Games,
            BadgeModel played200Games,
            BadgeModel played500Games)
	    {
	        PlayedOneGame = playedOneGame;
	        PlayedTenGames = playedTenGames;
	        Played50Games = played50Games;
	        Played100Games = played100Games;
	        Played200Games = played200Games;
	        Played500Games = played500Games;
	    }
    }

    public class BadgeModel
    {
        public string Description { get; private set; }
        public string CssClass { get; private set; }
        
        public BadgeModel(string description, string cssClass)
        {
            Description = description;
            CssClass = cssClass;
        }
    }

    public interface IBadgeModelFactory
    {
        BadgeModel Create(string description, bool wasEarned);
    }

    public class BadgeModelFactory : IBadgeModelFactory
    {
        public BadgeModel Create(string description, bool wasEarned)
        {
            var cssClass = wasEarned ? "icon-check" : "icon-check-empty";
            return new BadgeModel(description, cssClass);
        }
    }
}