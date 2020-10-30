using System.Collections.Generic;
using System.Linq;
using Core.Entities;
using Core.Services;
using PokerBunch.Client.Clients;
using PokerBunch.Client.Models.Request;
using ApiPlayer = PokerBunch.Client.Models.Response.Player;

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