using System.Collections.Generic;
using Infrastructure.Api.Connection;
using Infrastructure.Api.Models;
using PokerBunch.Common.Urls.ApiUrls;

namespace Infrastructure.Api.Clients
{
    public class PlayerClient : ApiClient
    {
        public PlayerClient(ApiConnection apiConnection) : base(apiConnection)
        {
        }

        public ApiPlayer Get(string id)
        {
            return ApiConnection.Get<ApiPlayer>(new ApiPlayerUrl(id));
        }

        public IList<ApiPlayer> List(string bunchId)
        {
            return ApiConnection.Get<IList<ApiPlayer>>(new ApiBunchPlayersUrl(bunchId));
        }

        public ApiPlayer Add(ApiPlayer player)
        {
            return ApiConnection.Post<ApiPlayer>(new ApiBunchPlayersUrl(player.BunchId), player);
        }

        public void Delete(string playerId)
        {
            ApiConnection.Delete(new ApiPlayerUrl(playerId));
        }

        public void Invite(ApiInvite apiInvite)
        {
            ApiConnection.Post(new ApiPlayerInviteUrl(apiInvite.PlayerId), apiInvite);
        }
    }
}