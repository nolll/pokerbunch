using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Core.UseCases.PlayerList;

namespace Api.Models
{
    [CollectionDataContract(Namespace = "", Name = "players", ItemName = "player")]
    public class ApiPlayerList : List<ApiPlayer>
    {
        public ApiPlayerList(PlayerListResult playerListResult)
        {
            AddRange(playerListResult.Players.Select(o => new ApiPlayer(o.Name)));
        }

        public ApiPlayerList()
        {
        }
    }
}