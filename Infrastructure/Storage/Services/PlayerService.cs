using System.Collections.Generic;
using System.Linq;
using Core.Entities;
using Core.Services;
using JetBrains.Annotations;

namespace Infrastructure.Storage.Services
{
    public class PlayerService : BaseService, IPlayerService
    {
        private readonly ApiConnection _api;

        public PlayerService(ApiConnection api)
        {
            _api = api;
        }

        public Player Get(string id)
        {
            var url = Url.Player(id);
            var apiPlayer = _api.Get<ApiPlayer>(url);
            return CreatePlayer(apiPlayer);
        }

        public IList<Player> List(string bunchId)
        {
            var url = Url.PlayersByBunch(bunchId);
            var apiPlayers = _api.Get<IList<ApiPlayer>>(url);
            return apiPlayers.Select(CreatePlayer).ToList();
        }
        
        public string Add(Player player)
        {
            var postPlayer = new ApiPlayer(player.BunchId, player.UserId, player.UserName, player.DisplayName, (int)player.Role, player.Color);
            var apiLocation = _api.Post<ApiPlayer>(Url.PlayersByBunch(player.BunchId), postPlayer);
            return CreatePlayer(apiLocation).Id;
        }

        public void Delete(string playerId)
        {
            _api.Delete(Url.Player(playerId));
        }

        public void Invite(string playerId, string email)
        {
            var apiInvite = new ApiInvite(email);
            _api.Post(Url.Invite(playerId), apiInvite);
        }

        private Player CreatePlayer(ApiPlayer p)
        {
            return new Player(p.BunchId, p.Id, p.UserId, p.UserName, p.Name, (Role)p.RoleId, p.Color);
        }

        private class ApiPlayer
        {
            [UsedImplicitly]
            public string BunchId { get; set; }
            [UsedImplicitly]
            public string Id { get; set; }
            [UsedImplicitly]
            public string UserId { get; set; }
            [UsedImplicitly]
            public string UserName { get; set; }
            [UsedImplicitly]
            public string Name { get; set; }
            [UsedImplicitly]
            public int RoleId { get; set; }
            [UsedImplicitly]
            public string Color { get; set; }

            public ApiPlayer(string bunchId, string userId, string userName, string name, int roleId, string color)
            {
                BunchId = bunchId;
                UserId = userId;
                UserName = userName;
                Name = name;
                RoleId = roleId;
                Color = color;
            }

            public ApiPlayer()
            {
            }
        }

        private class ApiInvite
        {
            [UsedImplicitly]
            public string Email { get; set; }

            public ApiInvite(string email)
            {
                Email = email;
            }
        }
    }
}