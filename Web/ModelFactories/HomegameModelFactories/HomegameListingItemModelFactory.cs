using Core.Classes;
using Core.Services;
using Web.Models.HomegameModels.Listing;

namespace Web.ModelFactories.HomegameModelFactories
{
    public class HomegameListingItemModelFactory : IHomegameListingItemModelFactory
    {
        private readonly IUrlProvider _urlProvider;

        public HomegameListingItemModelFactory(IUrlProvider urlProvider)
        {
            _urlProvider = urlProvider;
        }

        public HomegameListingItemModel Create(Homegame homegame)
        {
            return new HomegameListingItemModel
                {
                    Name = homegame.DisplayName,
                    UrlModel = _urlProvider.GetHomegameDetailsUrl(homegame)
                };
        }
    }
}