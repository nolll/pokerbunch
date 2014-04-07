using System.Collections.Generic;
using System.Linq;
using Application.Services;
using Core.UseCases;
using Web.Models.HomegameModels.List;

namespace Web.ModelFactories.HomegameModelFactories
{
    public class BunchListItemModelFactory : IBunchListItemModelFactory
    {
        private readonly IUrlProvider _urlProvider;

        public BunchListItemModelFactory(IUrlProvider urlProvider)
        {
            _urlProvider = urlProvider;
        }

        public BunchListItemModel Create(BunchItem bunchItem)
        {
            return new BunchListItemModel
            {
                Name = bunchItem.DisplayName,
                Url = _urlProvider.GetHomegameDetailsUrl(bunchItem.Slug)
            };
        }

        public IList<BunchListItemModel> CreateList(IList<BunchItem> bunchItems)
        {
            return bunchItems.Select(Create).ToList();
        }
    }
}