using System;
using System.Collections.Generic;
using System.Linq;
using Core.Entities;
using Core.Services;

namespace Core.Factories
{
    public static class CashgameFactory
    {
        public static Cashgame Create(
            int id,
            int homegameId,
            string location,
            GameStatus status,
            bool isStarted,
            DateTime? startTime,
            DateTime? endTime,
            IList<CashgameResult> results,
            int playerCount,
            int diff,
            int turnover,
            bool hasActivePlayers,
            int totalStacks,
            int averageBuyin,
            string dateString)
        {
            return new Cashgame(
                id,
                homegameId,
                location,
                status,
                isStarted,
                startTime,
                endTime,
                results,
                playerCount,
                diff,
                turnover,
                hasActivePlayers,
                totalStacks,
                averageBuyin,
                dateString);
        }

        public static Cashgame Create(string location, int homegameId, int? status = null, int? id = null, IList<CashgameResult> results = null)
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
            var dateString = startTime.HasValue ? Globalization.FormatIsoDate(startTime.Value) : string.Empty;

            return Create(
                id.HasValue ? id.Value : 0,
                homegameId,
                location,
                status.HasValue ? (GameStatus)status.Value : GameStatus.Created,
                startTime.HasValue,
                startTime,
                endTime,
                results,
                playerCount,
                buyinSum - cashoutSum,
                buyinSum,
                HasActivePlayers(results),
                GetTotalStacks(results),
                GetAverageBuyin(buyinSum, playerCount),
                dateString);
        }

        private static DateTime? GetStartTime(IEnumerable<CashgameResult> results)
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

        private static DateTime? GetEndTime(IEnumerable<CashgameResult> results)
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

        private static int GetBuyinSum(IEnumerable<CashgameResult> results)
        {
            return results.Sum(result => result.Buyin);
        }

        private static int GetCashoutSum(IEnumerable<CashgameResult> results)
        {
            return results.Sum(result => result.Stack);
        }

        private static bool HasActivePlayers(IEnumerable<CashgameResult> results)
        {
            return results.Any(result => !result.CashoutTime.HasValue);
        }

        private static int GetTotalStacks(IEnumerable<CashgameResult> results)
        {
            return results.Sum(result => result.Stack);
        }

        private static int GetAverageBuyin(int turnover, int playerCount)
        {
            if (playerCount == 0)
            {
                return 0;
            }
            return (int) Math.Round(turnover/(double) playerCount);
        }
    }
}