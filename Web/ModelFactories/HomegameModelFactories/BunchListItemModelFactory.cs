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

        public BunchListItemModel Create(BunchListItem bunchListItem)
        {
            return new BunchListItemModel
            {
                Name = bunchListItem.DisplayName,
                Url = _urlProvider.GetHomegameDetailsUrl(bunchListItem.Slug)
            };
        }

        public IList<BunchListItemModel> CreateList(IList<BunchListItem> bunchItems)
        {
            return bunchItems.Select(Create).ToList();
        }
    }
}