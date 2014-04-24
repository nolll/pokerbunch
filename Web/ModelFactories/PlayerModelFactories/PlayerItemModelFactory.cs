using System.Collections.Generic;
using System.Linq;
using Application.Services;
using Application.UseCases.PlayerList;
using Web.Models.PlayerModels.List;

namespace Web.ModelFactories.PlayerModelFactories
{
    public class PlayerItemModelFactory : IPlayerItemModelFactory
    {
        private readonly IUrlProvider _urlProvider;

        public PlayerItemModelFactory(IUrlProvider urlProvider)
        {
            _urlProvider = urlProvider;
        }

        public PlayerItemModel Create(string slug, PlayerListItem p)
        {
            return new PlayerItemModel
            {
                Name = p.Name,
                Url = _urlProvider.GetPlayerDetailsUrl(slug, p.Name)
            };
        }

        public IList<PlayerItemModel> CreateList(string slug, IList<PlayerListItem> items)
        {
            return items.Select(item => Create(slug, item)).ToList();
        }
    }
}