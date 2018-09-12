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

        public IList<ListCashgame> List(string bunchId, int? year = null)
        {
            return FakeData.Cashgames
                .Where(o => o.Bunch.Id == bunchId)
                .Select(o => 
                    new ListCashgame(
                        o.Id,
                        o.StartTime, 
                        o.UpdatedTime, 
                        o.IsRunning, 
                        new SmallLocation(o.Location.Id, o.Location.Name),
                        o.Players.Select(p => 
                            new ListCashgame.CashgamePlayer(
                                p.Id,
                                p.Name,
                                p.Color,
                                p.Stack,
                                p.Buyin,
                                p.StartTime,
                                p.UpdatedTime)).ToList())).ToList();
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

        public IList<int> GetYears(string bunchId)
        {
            return new List<int>
            {
                2018
            };
        }

        public void Report(string cashgameId, string playerId, int stack)
        {
            throw new NotImplementedException();
        }

        public void Buyin(string cashgameId, string playerId, int added, int stack)
        {
            throw new NotImplementedException();
        }

        public void Cashout(string cashgameId, string playerId, int stack)
        {
            throw new NotImplementedException();
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