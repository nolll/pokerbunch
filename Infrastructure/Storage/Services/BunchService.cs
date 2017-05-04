using System;
using System.Collections.Generic;
using System.Linq;
using Core.Entities;
using Core.Services;
using JetBrains.Annotations;

namespace Infrastructure.Storage.Services
{
    public class BunchService : BaseService, IBunchService
    {
        private readonly ApiConnection _api;

        public BunchService(ApiConnection api)
        {
            _api = api;
        }

        public Bunch Get(string id)
        {
            var apiBunch = _api.Get<ApiBunch>(Url.Bunch(id));
            return ToBunch(apiBunch);
        }

        public IList<SmallBunch> List()
        {
            var apiBunches = _api.Get<IList<ApiSmallBunch>>(Url.Bunches);
            return apiBunches.Select(ToSmallBunch).ToList();
        }

        public IList<SmallBunch> ListForUser()
        {
            var apiBunches = _api.Get<IList<ApiSmallBunch>>(Url.BunchesByUser);
            return apiBunches.Select(ToSmallBunch).ToList();
        }

        public Bunch Add(Bunch bunch)
        {
            var postBunch = new ApiBunch(bunch);
            var apiBunch = _api.Post<ApiBunch>(Url.Bunches, postBunch);
            return ToBunch(apiBunch);
        }

        public Bunch Update(Bunch bunch)
        {
            var id = bunch.Id;
            var postBunch = new ApiBunch(bunch);
            var apiBunch = _api.Post<ApiBunch>(Url.Bunch(id), postBunch);
            return ToBunch(apiBunch);
        }

        public void Join(string id, string code)
        {
            var apiJoin = new ApiJoin(code);
            _api.Post(Url.Join(id), apiJoin);
        }

        private Bunch ToBunch(ApiBunch b)
        {
            var timezone = TimeZoneInfo.FindSystemTimeZoneById(b.Timezone);
            var currency = new Currency(b.CurrencySymbol, b.CurrencyLayout);
            var role = ParseRole(b.Role);
            var id = b.Player?.Id ?? null;
            return new Bunch(b.Id, b.Name, b.Description, b.HouseRules, timezone, b.DefaultBuyin, currency, role, id);
        }

        private SmallBunch ToSmallBunch(ApiSmallBunch b)
        {
            return new SmallBunch(b.Id, b.Name, b.Description);
        }

        private Role ParseRole(string role)
        {
            if (role == "admin")
                return Role.Admin;
            if (role == "manager")
                return Role.Manager;
            if (role == "player")
                return Role.Player;
            if (role == "guest")
                return Role.Guest;
            return Role.None;
        }

        private class ApiBunch : ApiSmallBunch
        {
            [UsedImplicitly]
            public string HouseRules { get; set; }
            [UsedImplicitly]
            public string Timezone { get; set; }
            [UsedImplicitly]
            public string CurrencySymbol { get; set; }
            [UsedImplicitly]
            public string CurrencyLayout { get; set; }
            [UsedImplicitly]
            public int DefaultBuyin { get; set; }
            [UsedImplicitly]
            public ApiBunchPlayer Player { get; set; }
            [UsedImplicitly]
            public string Role { get; set; }

            public ApiBunch(Bunch b)
                : base(b)
            {
                HouseRules = b.HouseRules;
                Timezone = b.Timezone.Id;
                CurrencySymbol = b.Currency.Symbol;
                CurrencyLayout = b.Currency.Layout;
                DefaultBuyin = b.DefaultBuyin;
                Player = new ApiBunchPlayer();
                Role = b.Role.ToString().ToLower();
            }

            public ApiBunch()
            {
            }
        }

        private class ApiBunchPlayer
        {
            [UsedImplicitly]
            public string Id { get; set; }
            [UsedImplicitly]
            public string Name { get; set; }
        }

        private class ApiSmallBunch
        {
            [UsedImplicitly]
            public string Id { get; set; }
            [UsedImplicitly]
            public string Name { get; set; }
            [UsedImplicitly]
            public string Description { get; set; }

            public ApiSmallBunch(Bunch b)
            {
                Id = b.Id;
                Name = b.DisplayName;
                Description = b.Description;
            }

            public ApiSmallBunch()
            {
            }
        }

        private class ApiJoin
        {
            [UsedImplicitly]
            public string Code { get; set; }

            public ApiJoin(string code)
            {
                Code = code;
            }
        }
    }
}