using Application.Services;
using Core.Classes;
using Web.Models.NavigationModels;

namespace Web.ModelFactories.NavigationModelFactories
{
    public class HomegameNavigationModelFactory : IHomegameNavigationModelFactory
    {
        private readonly IUrlProvider _urlProvider;

        public HomegameNavigationModelFactory(IUrlProvider urlProvider)
        {
            _urlProvider = urlProvider;
        }

        public HomegameNavigationModel Create(Homegame homegame)
        {
            return new HomegameNavigationModel
                {
                    Heading = homegame.DisplayName,
			        HeadingLink = _urlProvider.GetHomegameDetailsUrl(homegame.Slug),
			        CashgameLink = _urlProvider.GetCashgameIndexUrl(homegame.Slug),
                    PlayerLink = _urlProvider.GetPlayerIndexUrl(homegame.Slug)
                };
        }
    }
}