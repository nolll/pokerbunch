using System.Collections.Generic;
using PokerBunch.Client.Connection;
using PokerBunch.Client.Models;
using PokerBunch.Client.Models.Request;
using PokerBunch.Client.Models.Response;
using PokerBunch.Common.Urls.ApiUrls;

namespace PokerBunch.Client.Clients
{
    public class PlayerClient : ApiClient
    {
        public PlayerClient(ApiConnection apiConnection) : base(apiConnection)
        {
        }

        public Player Get(string id)
        {
            return ApiConnection.Get<Player>(new ApiPlayerUrl(id));
        }

        public IList<Player> List(string bunchId)
        {
            return ApiConnection.Get<IList<Player>>(new ApiBunchPlayersUrl(bunchId));
        }

        public void Delete(string playerId)
        {
            ApiConnection.Delete(new ApiPlayerUrl(playerId));
        }

        public void Invite(PlayerInvite playerInvite)
        {
            ApiConnection.Post(new ApiPlayerInviteUrl(playerInvite.PlayerId), playerInvite);
        }
    }
}