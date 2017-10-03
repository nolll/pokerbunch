using System;
using System.Collections.Generic;
using System.Linq;
using Core.Entities;
using Core.Services;
using Infrastructure.Api.Connection;
using Infrastructure.Api.Models;
using PokerBunch.Common.Urls.ApiUrls;

namespace Infrastructure.Api.Services
{
    public class BunchService : BaseService, IBunchService
    {
        public BunchService(ApiConnection apiClient) : base(apiClient)
        {
        }

        public Bunch Get(string id)
        {
            var apiBunch = _api.Get<ApiBunch>(new ApiBunchUrl(id));
            return ToBunch(apiBunch);
        }

        public IList<SmallBunch> List()
        {
            var apiBunches = _api.Get<IList<ApiSmallBunch>>(new ApiBunchesUrl());
            return apiBunches.Select(ToSmallBunch).ToList();
        }

        public IList<SmallBunch> ListForUser()
        {
            var apiBunches = _api.Get<IList<ApiSmallBunch>>(new ApiUserBunchesUrl());
            return apiBunches.Select(ToSmallBunch).ToList();
        }

        public Bunch Add(Bunch bunch)
        {
            var postBunch = new ApiBunch(bunch);
            var apiBunch = _api.Post<ApiBunch>(new ApiBunchesUrl(), postBunch);
            return ToBunch(apiBunch);
        }

        public Bunch Update(Bunch bunch)
        {
            var id = bunch.Id;
            var postBunch = new ApiBunch(bunch);
            var apiBunch = _api.Post<ApiBunch>(new ApiBunchUrl(id), postBunch);
            return ToBunch(apiBunch);
        }

        public void Join(string id, string code)
        {
            var apiJoin = new ApiJoin(code);
            _api.Post(new ApiBunchJoinUrl(id), apiJoin);
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