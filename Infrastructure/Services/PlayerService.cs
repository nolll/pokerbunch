using System.Collections.Generic;
using System.Linq;
using Core.Entities;
using Core.Services;
using PokerBunch.Client.Clients;
using PokerBunch.Client.Models;
using PokerBunch.Client.Models.Request;

namespace Infrastructure.Api.Services
{
    public class PlayerService : BaseService, IPlayerService
    {
        public PlayerService(PokerBunchClient apiClient) : base(apiClient)
        {
        }

        public Player Get(string id)
        {
            var apiPlayer = ApiClient.Players.Get(id);
            return CreatePlayer(apiPlayer);
        }

        public IList<Player> List(string bunchId)
        {
            var apiPlayers = ApiClient.Players.List(bunchId);
            return apiPlayers.Select(CreatePlayer).ToList();
        }
        
        public string Add(Player player)
        {
            var postPlayer = new ApiPlayer(player.BunchId, player.UserId, player.UserName, player.DisplayName, (int)player.Role, player.Color);
            var returnedPlayer = ApiClient.Players.Add(postPlayer);
            return CreatePlayer(returnedPlayer).Id;
        }

        public void Delete(string playerId)
        {
            ApiClient.Players.Delete(playerId);
        }

        public void Invite(string playerId, string email)
        {
            var apiInvite = new PlayerInvite(playerId, email);
            ApiClient.Players.Invite(apiInvite);
        }

        private Player CreatePlayer(ApiPlayer p)
        {
            return new Player(p.BunchId, p.Id, p.UserId, p.UserName, p.Name, (Role)p.RoleId, p.Color);
        }
    }
}