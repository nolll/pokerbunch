using Core.UseCases;
using PokerBunch.Common.Urls.SiteUrls;
using Web.Extensions;

namespace Web.Models.PlayerModels.List
{
    public class PlayerItemModel : IViewModel
    {
        public string Name { get; }
        public string Url { get; }
        public string Color { get; }

        public PlayerItemModel(PlayerList.PlayerListItem p)
        {
            Name = p.Name;
            Url = new PlayerDetailsUrl(p.Id).Relative;
            Color = p.Color;
        }

        public View GetView()
        {
            return new View("PlayerList/Item");
        }
    }
}