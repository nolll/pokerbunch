using System;
using System.Collections.Generic;
using System.Linq;
using Core.Entities;
using Core.Services;
using PokerBunch.Client.Models.Response;

namespace Infrastructure.Api.FakeServices
{
    public class FakeCashgameService : ICashgameService
    {
        public DetailedCashgame GetDetailedById(string id)
        {
            throw new NotImplementedException();
        }

        public DetailedCashgame GetCurrent(string bunchId)
        {
            return FakeData.Cashgames.FirstOrDefault(o => o.IsRunning && o.Bunch.Id == bunchId);
        }

        public IList<ListCashgame> EventList(string eventId)
        {
            throw new NotImplementedException();
        }

        public IList<ListCashgame> PlayerList(string playerId)
        {
            throw new NotImplementedException();
        }

        public void DeleteGame(string id)
        {
            throw new NotImplementedException();
        }

        public string Add(string bunchId, string locationId)
        {
            throw new NotImplementedException();
        }

        public DetailedCashgame Update(string id, string locationId, string eventId)
        {
            throw new NotImplementedException();
        }

        public void Report(string cashgameId, string playerId, int stack)
        {
        }

        public void Buyin(string cashgameId, string playerId, int added, int stack)
        {
        }

        public void Cashout(string cashgameId, string playerId, int stack)
        {
        }

        public void UpdateAction(string cashgameId, string actionId, DateTime timestamp, int stack, int added)
        {
            throw new NotImplementedException();
        }

        public void DeleteAction(string cashgameId, string actionId)
        {
            throw new NotImplementedException();
        }
    }
}