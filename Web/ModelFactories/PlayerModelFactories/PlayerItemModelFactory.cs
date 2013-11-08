using Core.Classes;
using Core.Services;
using Web.Models.PlayerModels.Listing;

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
                    UrlModel = _urlProvider.GetPlayerDetailsUrl(homegame, player)
                };
        }
    }
}