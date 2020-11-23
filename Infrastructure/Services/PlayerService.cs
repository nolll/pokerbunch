using Core.Entities;
using Core.Services;
using PokerBunch.Client.Clients;
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

        private Player CreatePlayer(ApiPlayer p)
        {
            return new Player(p.BunchId, p.Id, p.UserId, p.UserName, p.Name, (Role)p.RoleId, p.Color);
        }
    }
}