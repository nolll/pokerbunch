using System;
using Core.Entities;
using Core.Services;
using PokerBunch.Client.Clients;
using PokerBunch.Client.Models.Request;
using ApiBunch = PokerBunch.Client.Models.Response.Bunch;
using Bunch = Core.Entities.Bunch;

namespace Infrastructure.Api.Services
{
    public class BunchService : BaseService, IBunchService
    {
        public BunchService(PokerBunchClient apiClient) : base(apiClient)
        {
        }

        public Bunch Get(string id)
        {
            var apiBunch = ApiClient.Bunches.Get(id);
            return ToBunch(apiBunch);
        }

        public Bunch Update(Bunch bunch)
        {
            var postBunch = new BunchUpdate(bunch.Id, bunch.Description, bunch.HouseRules, bunch.Timezone.Id, bunch.Currency.Symbol, bunch.Currency.Layout, bunch.DefaultBuyin);
            var apiBunch = ApiClient.Bunches.Update(postBunch);
            return ToBunch(apiBunch);
        }

        private Bunch ToBunch(ApiBunch b)
        {
            var timezone = TimeZoneInfo.FindSystemTimeZoneById(b.Timezone);
            var currency = new Currency(b.CurrencySymbol, b.CurrencyLayout);
            var role = ParseRole(b.Role);
            var id = b.Player?.Id;
            return new Bunch(b.Id, b.Name, b.Description, b.HouseRules, timezone, b.DefaultBuyin, currency, role, id);
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
    }
}