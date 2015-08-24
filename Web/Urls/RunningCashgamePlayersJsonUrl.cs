using Core.Urls;

namespace Web.Urls
{
    public class RunningCashgamePlayersJsonUrl : SlugUrl
    {
        public RunningCashgamePlayersJsonUrl(string slug)
            : base(Routes.RunningCashgamePlayersJson, slug)
        {
        }
    }
}