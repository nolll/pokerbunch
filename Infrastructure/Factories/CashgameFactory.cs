using System;
using System.Collections.Generic;
using Core.Classes;

namespace Infrastructure.Factories
{

    public class CashgameFactory : ICashgameFactory
    {

        public Cashgame Create(string location, GameStatus? status = null, int? id = null,
                               List<CashgameResult> results = null)
        {
            if (results == null)
            {
                results = new List<CashgameResult>();
            }

            var playerCount = results.Count;
            var startTime = GetStartTime(results);
            var endTime = GetEndTime(results);
            var buyinSum = GetBuyinSum(results);
            var cashoutSum = GetCashoutSum(results);

            var cashgame = new Cashgame
                {
                    Location = location,
                    Status = status.HasValue ? status.Value : GameStatus.Created,
                    Id = id.HasValue ? id.Value : 0,
                    Results = results,
                    PlayerCount = playerCount,
                    StartTime = startTime,
                    EndTime = endTime,
                    Duration = GetDuration(startTime, endTime),
                    IsStarted = startTime.HasValue,
                    Turnover = buyinSum,
                    Diff = buyinSum - cashoutSum,
                    HasActivePlayers = HasActivePlayers(results),
                    TotalStacks = GetTotalStacks(results),
                    AverageBuyin = GetAverageBuyin(buyinSum, playerCount)
                };

            return cashgame;
        }

        private DateTime? GetStartTime(List<CashgameResult> results)
        {
            DateTime? startTime = null;
            foreach (var result in results)
            {
                if (!startTime.HasValue || result.BuyinTime < startTime)
                {
                    startTime = result.BuyinTime;
                }
            }
            return startTime;
        }

        private DateTime? GetEndTime(List<CashgameResult> results)
        {
            DateTime? endTime = null;
            foreach (var result in results)
            {
                if (!endTime.HasValue || result.CashoutTime > endTime)
                {
                    endTime = result.CashoutTime;
                }
            }
            return endTime;
        }

        private int GetDuration(DateTime? startTime = null, DateTime? endTime = null)
        {
            if (!startTime.HasValue || !endTime.HasValue)
            {
                return 0;
            }
            var timespan = endTime - startTime;
            return (int) Math.Round(timespan.Value.TotalMinutes);
        }

        private int GetBuyinSum(List<CashgameResult> results)
        {
            var buyinSum = 0;
            foreach (var result in results)
            {
                buyinSum += result.Buyin;
            }
            return buyinSum;
        }

        private int GetCashoutSum(List<CashgameResult> results)
        {
            var cashoutSum = 0;
            foreach (var result in results)
            {
                cashoutSum += result.Stack;
            }
            return cashoutSum;
        }

        private bool HasActivePlayers(List<CashgameResult> results)
        {
            foreach (var result in results)
            {
                if (!result.CashoutTime.HasValue)
                {
                    return true;
                }
            }
            return false;
        }

        private int GetTotalStacks(List<CashgameResult> results)
        {
            var sum = 0;
            foreach (var result in results)
            {
                sum += result.Stack;
            }
            return sum;
        }

        private int GetAverageBuyin(int turnover, int playerCount)
        {
            if (playerCount == 0)
            {
                return 0;
            }
            return (int) Math.Round((double) turnover/(double) playerCount);
        }

    }

}