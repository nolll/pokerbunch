using System.Collections.Generic;
using System.Linq;
using Core.Entities;
using Core.Services;
using Infrastructure.Api.Connection;
using PokerBunch.Common.Urls.ApiUrls;

namespace Infrastructure.Api.Services
{
    public class PlayerService : IPlayerService
    {
        private readonly ApiConnection _api;

        public PlayerService(ApiConnection api)
        {
            _api = api;
        }

        public Player Get(string id)
        {
            var apiPlayer = _api.Get<ApiPlayer>(new ApiPlayerUrl(id));
            return CreatePlayer(apiPlayer);
        }

        public IList<Player> List(string bunchId)
        {
            var apiPlayers = _api.Get<IList<ApiPlayer>>(new ApiBunchPlayersUrl(bunchId));
            return apiPlayers.Select(CreatePlayer).ToList();
        }
        
        public string Add(Player player)
        {
            var postPlayer = new ApiPlayer(player.BunchId, player.UserId, player.UserName, player.DisplayName, (int)player.Role, player.Color);
            var apiLocation = _api.Post<ApiPlayer>(new ApiBunchPlayersUrl(player.BunchId), postPlayer);
            return CreatePlayer(apiLocation).Id;
        }

        public void Delete(string playerId)
        {
            _api.Delete(new ApiPlayerUrl(playerId));
        }

        public void Invite(string playerId, string email)
        {
            var apiInvite = new ApiInvite(email);
            _api.Post(new ApiPlayerInviteUrl(playerId), apiInvite);
        }

        private Player CreatePlayer(ApiPlayer p)
        {
            return new Player(p.BunchId, p.Id, p.UserId, p.UserName, p.Name, (Role)p.RoleId, p.Color);
        }
    }
}