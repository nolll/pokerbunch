using System.Collections.Generic;
using Tests.Core.UseCases;
using Web.Models.PlayerModels.List;

namespace Web.ModelFactories.PlayerModelFactories
{
    public interface IPlayerItemModelFactory
    {
        PlayerItemModel Create(string slug, PlayerListItem playerListItem);
        IList<PlayerItemModel> CreateList(string slug, IList<PlayerListItem> items);
    }
}