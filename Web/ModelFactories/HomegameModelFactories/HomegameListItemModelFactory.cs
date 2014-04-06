using Application.Services;
using Core.UseCases;
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

        public BunchListItemModel Create(BunchItem bunchItem)
        {
            return new BunchListItemModel
            {
                Name = bunchItem.DisplayName,
                UrlModel = _urlProvider.GetHomegameDetailsUrl(bunchItem.Slug)
            };
        }
    }
}