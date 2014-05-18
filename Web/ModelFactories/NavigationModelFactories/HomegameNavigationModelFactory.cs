using Application.Services;
using Application.UseCases.CashgameContext;
using Core.Entities;
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
            return Create(homegame.Slug, homegame.DisplayName);
        }

        public HomegameNavigationModel Create(BunchContextResult bunchContextResult)
        {
            return Create(bunchContextResult.Slug, bunchContextResult.BunchName);
        }

        private HomegameNavigationModel Create(string slug, string bunchName)
        {
            return new HomegameNavigationModel
            {
                Heading = bunchName,
                HeadingLink = _urlProvider.GetHomegameDetailsUrl(slug),
                CashgameLink = _urlProvider.GetCashgameIndexUrl(slug),
                PlayerLink = _urlProvider.GetPlayerIndexUrl(slug)
            };
        }
    }
}