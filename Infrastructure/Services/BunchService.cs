using System;
using System.Collections.Generic;
using System.Linq;
using Core.Entities;
using Core.Services;
using PokerBunch.Client.Clients;
using PokerBunch.Client.Models;
using PokerBunch.Client.Models.Request;
using PokerBunch.Common.Urls.ApiUrls;

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

        public IList<SmallBunch> List()
        {
            var apiBunches = ApiClient.Bunches.List();
            return apiBunches.Select(ToSmallBunch).ToList();
        }

        public IList<SmallBunch> ListForUser()
        {
            var apiBunches = ApiClient.Bunches.ListForUser();
            return apiBunches.Select(ToSmallBunch).ToList();
        }

        public Bunch Add(Bunch bunch)
        {
            var postBunch = new ApiBunchAdd(bunch.DisplayName, bunch.Description, bunch.Timezone.Id, bunch.Currency.Symbol, bunch.Currency.Layout);
            var apiBunch = ApiClient.Bunches.Add(postBunch);
            return ToBunch(apiBunch);
        }

        public Bunch Update(Bunch bunch)
        {
            var postBunch = new ApiBunchUpdate(bunch.Id, bunch.Description, bunch.HouseRules, bunch.Timezone.Id, bunch.Currency.Symbol, bunch.Currency.Layout, bunch.DefaultBuyin);
            var apiBunch = ApiClient.Bunches.Update(postBunch);
            return ToBunch(apiBunch);
        }

        public void Join(string bunchId, string code)
        {
            var apiJoin = new ApiJoin(bunchId, code);
            ApiClient.Bunches.Join(apiJoin);
        }

        private Bunch ToBunch(ApiBunch b)
        {
            var timezone = TimeZoneInfo.FindSystemTimeZoneById(b.Timezone);
            var currency = new Currency(b.CurrencySymbol, b.CurrencyLayout);
            var role = ParseRole(b.Role);
            var id = b.Player?.Id;
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
    }
}