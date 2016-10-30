using System;
using System.Collections.Generic;
using System.Linq;
using Core.Entities;
using Core.Repositories;
using JetBrains.Annotations;

namespace Infrastructure.Storage.Repositories
{
    public class ApiBunchRepository : IBunchRepository
    {
        private readonly ApiConnection _apiConnection;

        public ApiBunchRepository(ApiConnection apiConnection)
        {
            _apiConnection = apiConnection;
        }

        public Bunch Get(string id)
        {
            var apiBunch = _apiConnection.Get<ApiBunch>($"bunches/{id}");
            return ToBunch(apiBunch);
        }

        public IList<SmallBunch> List()
        {
            var apiBunches = _apiConnection.Get<IList<ApiSmallBunch>>("bunches");
            return apiBunches.Select(ToSmallBunch).ToList();
        }

        public IList<SmallBunch> ListForUser()
        {
            var apiBunches = _apiConnection.Get<IList<ApiSmallBunch>>("user/bunches");
            return apiBunches.Select(ToSmallBunch).ToList();
        }

        public Bunch Add(Bunch bunch)
        {
            var postBunch = new ApiBunch(bunch);
            var apiBunch = _apiConnection.Post<ApiBunch>("bunches", postBunch);
            return ToBunch(apiBunch);
        }

        public Bunch Update(Bunch bunch)
        {
            var slug = bunch.Id;
            var postBunch = new ApiBunch(bunch);
            var apiBunch = _apiConnection.Post<ApiBunch>($"bunches/{slug}", postBunch);
            return ToBunch(apiBunch);
        }

        private Bunch ToBunch(ApiBunch b)
        {
            var timezone = TimeZoneInfo.FindSystemTimeZoneById(b.Timezone);
            var currency = new Currency(b.CurrencySymbol, b.CurrencyLayout);
            var role = ParseRole(b.Role);
            return new Bunch(b.Id, b.Name, b.Description, b.HouseRules, timezone, b.DefaultBuyin, currency, role);
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
            public string Role { get; set; }

            public ApiBunch(Bunch b)
                : base(b)
            {
                HouseRules = b.HouseRules;
                Timezone = b.Timezone.Id;
                CurrencySymbol = b.Currency.Symbol;
                CurrencyLayout = b.Currency.Layout;
                DefaultBuyin = b.DefaultBuyin;
                Role = b.Role.ToString().ToLower();
            }

            public ApiBunch()
            {
            }
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
    }
}