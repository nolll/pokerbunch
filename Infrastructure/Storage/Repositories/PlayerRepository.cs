using System;
using System.Collections.Generic;
using System.Linq;
using Core.Entities;
using Core.Repositories;
using Core.Services;
using Infrastructure.Storage.SqlDb;
using JetBrains.Annotations;

namespace Infrastructure.Storage.Repositories
{
    public class PlayerRepository : ApiRepository, IPlayerRepository
    {
        private readonly SqlPlayerDb _playerDb;
        private readonly ApiConnection _api;
        private readonly ICacheContainer _cacheContainer;

        public PlayerRepository(ApiConnection api, SqlServerStorageProvider db, ICacheContainer cacheContainer)
        {
            _playerDb = new SqlPlayerDb(db);
            _api = api;
            _cacheContainer = cacheContainer;
        }

        public Player Get(string id)
        {
            var apiEvent = _api.Get<ApiPlayer>(Url.Player(id));
            return CreatePlayer(apiEvent);
        }

        private IList<Player> Get(IList<string> ids)
        {
            return _cacheContainer.GetAndStore(_playerDb.Get, ids, TimeSpan.FromMinutes(CacheTime.Long));
        }

        public IList<Player> List(string bunchId)
        {
            var ids = _playerDb.Find(bunchId);
            return Get(ids);
        }

        public Player GetByUser(string bunchId, string userId)
        {
            var ids = _playerDb.FindByUserId(bunchId, userId);
            if (!ids.Any())
                return null;
            return Get(ids).First();
        }

        public string Add(Player player)
        {
            return _playerDb.Add(player);
        }

        public bool JoinBunch(Player player, Bunch bunch, string userId)
        {
            return _playerDb.JoinHomegame(player, bunch, userId);
        }

        public void Delete(string playerId)
        {
            _playerDb.Delete(playerId);
            _cacheContainer.Remove<Player>(playerId);
        }

        public void Invite(string playerId, string email)
        {
            var apiInvite = new ApiInvite(email);
            _api.Post(Url.Invite(playerId), apiInvite);
        }

        private Player CreatePlayer(ApiPlayer l)
        {
            return new Player(l.BunchId, l.Id, l.UserId, l.DisplayName, (Role)l.RoleId, l.Color);
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
            public string DisplayName { get; set; }
            [UsedImplicitly]
            public int RoleId { get; set; }
            [UsedImplicitly]
            public string Color { get; set; }

            public ApiPlayer(string bunchId, string id, string userId, string displayName, int roleId, string color)
            {
                BunchId = bunchId;
                Id = id;
                UserId = userId;
                DisplayName = displayName;
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