using Application.Services;
using Core.Classes;
using Tests.Core.UseCases;
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

        public PlayerItemModel Create(Homegame homegame, Player player)
        {
            return new PlayerItemModel
            {
                Name = player.DisplayName,
                Url = _urlProvider.GetPlayerDetailsUrl(homegame.Slug, player.DisplayName)
            };
        }

        public PlayerItemModel Create(string slug, PlayerListItem p)
        {
            return new PlayerItemModel
            {
                Name = p.Name,
                Url = _urlProvider.GetPlayerDetailsUrl(slug, p.Name)
            };
        }
    }
}