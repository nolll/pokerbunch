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

        public HomegameListItemModel Create(BunchItem bunchItem)
        {
            return new HomegameListItemModel
            {
                Name = bunchItem.DisplayName,
                UrlModel = _urlProvider.GetHomegameDetailsUrl(bunchItem.Slug)
            };
        }
    }
}