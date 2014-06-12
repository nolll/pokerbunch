using Application.UseCases.BunchContext;
using Core.Entities;
using Web.Models.UrlModels;

namespace Web.Models.NavigationModels
{
    public class HomegameNavigationModel
    {
        public string Heading { get; private set; }
        public UrlModel HeadingUrl { get; private set; }
        public UrlModel CashgameUrl { get; private set; }
        public UrlModel PlayerUrl { get; private set; }

        public HomegameNavigationModel(Homegame homegame)
            : this(homegame.Slug, homegame.DisplayName)
        {
        }

        public HomegameNavigationModel(BunchContextResult bunchContextResult)
            : this(bunchContextResult.Slug, bunchContextResult.BunchName)
        {
        }

        private HomegameNavigationModel(string slug, string bunchName)
        {
            Heading = bunchName;
            HeadingUrl = new HomegameDetailsUrlModel(slug);
            CashgameUrl = new CashgameIndexUrlModel(slug);
            PlayerUrl = new PlayerIndexUrlModel(slug);
        }
    }
}
