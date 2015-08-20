using Core.Urls;

namespace Web.Urls
{
    public class RunningCashgameUrl : SlugUrl
    {
        public RunningCashgameUrl(string slug)
            : base(Routes.RunningCashgame, slug)
        {
        }
    }
}