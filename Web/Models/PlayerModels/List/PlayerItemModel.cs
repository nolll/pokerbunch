using Core.UseCases.PlayerList;

namespace Web.Models.PlayerModels.List
{
    public class PlayerItemModel
    {
        public string Name { get; private set; }
        public string Url { get; private set; }

        public PlayerItemModel(PlayerListItem p)
        {
            Name = p.Name;
            Url = p.Url.Relative;
        }
    }
}