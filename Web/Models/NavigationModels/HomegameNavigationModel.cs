using Application.UseCases.BunchContext;
using Core.Entities;
using Web.Models.UrlModels;

namespace Web.Models.NavigationModels
{
    public class HomegameNavigationModel
    {
        public string Heading { get; private set; }
        public Url HeadingUrl { get; private set; }
        public Url CashgameUrl { get; private set; }
        public Url PlayerUrl { get; private set; }

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
            HeadingUrl = new HomegameDetailsUrl(slug);
            CashgameUrl = new CashgameIndexUrl(slug);
            PlayerUrl = new PlayerIndexUrl(slug);
        }
    }
}
