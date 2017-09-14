using Core.UseCases;
using Web.Extensions;
using Web.Urls.SiteUrls;

namespace Web.Models.PlayerModels.List
{
    public class PlayerItemModel : IViewModel
    {
        public string Name { get; private set; }
        public string Url { get; private set; }
        public string Color { get; private set; }

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