using System.Collections.Generic;
using System.Linq;
using Application.Urls;
using Application.UseCases.PlayerList;
using Web.Models.PlayerModels.List;
using Web.Models.UrlModels;

namespace Web.ModelFactories.PlayerModelFactories
{
    public class PlayerItemModelFactory : IPlayerItemModelFactory
    {
        public PlayerItemModel Create(string slug, PlayerListItem p)
        {
            return new PlayerItemModel
            {
                Name = p.Name,
                Url = new PlayerDetailsUrl(slug, p.Id)
            };
        }

        public IList<PlayerItemModel> CreateList(string slug, IList<PlayerListItem> items)
        {
            return items.Select(item => Create(slug, item)).ToList();
        }
    }
}