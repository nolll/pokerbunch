using Core.Classes;
using Core.Services;
using Web.Models.HomegameModels.List;

namespace Web.ModelFactories.HomegameModelFactories
{
    public class HomegameListItemModelFactory : IHomegameListItemModelFactory
    {
        private readonly IUrlProvider _urlProvider;

        public HomegameListItemModelFactory(IUrlProvider urlProvider)
        {
            _urlProvider = urlProvider;
        }

        public HomegameListItemModel Create(Homegame homegame)
        {
            return new HomegameListItemModel
                {
                    Name = homegame.DisplayName,
                    UrlModel = _urlProvider.GetHomegameDetailsUrl(homegame)
                };
        }
    }
}