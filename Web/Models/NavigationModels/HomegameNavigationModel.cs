using Application.UseCases.BunchContext;
using Application.UseCases.CashgameContext;
using Core.Entities;
using Web.Models.UrlModels;
using Web.Services;

namespace Web.Models.NavigationModels
{
    public class HomegameNavigationModel
    {
        public string Heading { get; set; }
        public UrlModel HeadingLink { get; set; }
        public UrlModel CashgameLink { get; set; }
        public UrlModel PlayerLink { get; set; }

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
            HeadingLink = new HomegameDetailsUrlModel(slug);
            CashgameLink = new CashgameIndexUrlModel(slug);
            PlayerLink = new PlayerIndexUrlModel(slug);
        }
    }
}
