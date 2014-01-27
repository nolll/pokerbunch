using System;
using System.Collections.Generic;
using System.Linq;
using Core.Classes;
using Infrastructure.Data.Classes;
using Infrastructure.System;

namespace Infrastructure.Factories
{
    public class CashgameFactory : ICashgameFactory
    {
        private readonly ICashgameResultFactory _cashgameResultFactory;
        private readonly ICheckpointFactory _checkpointFactory;
        private readonly IGlobalization _globalization;

        public CashgameFactory(
            ICashgameResultFactory cashgameResultFactory,
            ICheckpointFactory checkpointFactory,
            IGlobalization globalization)
        {
            _cashgameResultFactory = cashgameResultFactory;
            _checkpointFactory = checkpointFactory;
            _globalization = globalization;
        }

        private Cashgame Create(RawCashgameWithResults rawGame)
        {
            var rawResults = rawGame.Results;
            var results = rawResults.Select(GetResultFromRawResult).ToList();
            return Create(rawGame.Location, rawGame.Status, rawGame.Id, results);
        }

        public Cashgame Create(RawCashgame rawGame, IEnumerable<RawCheckpoint> rawCheckpoints)
        {
            var rawResults = GetRawResults(rawCheckpoints);
            var results = rawResults.Select(GetResultFromRawResult).ToList();
            return Create(rawGame.Location, rawGame.Status, rawGame.Id, results);
        }

        public Cashgame Create(string location, int? status = null, int? id = null, IList<CashgameResult> results = null)
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
            var dateString = startTime.HasValue ? _globalization.FormatIsoDate(startTime.Value) : string.Empty;

            return new Cashgame(
                id.HasValue ? id.Value : 0,
                location,
                status.HasValue ? (GameStatus)status.Value : GameStatus.Created,
                startTime.HasValue,
                startTime,
                endTime,
                GetDuration(startTime, endTime),
                results,
                playerCount,
                buyinSum - cashoutSum,
                buyinSum,
                HasActivePlayers(results),
                GetTotalStacks(results),
                GetAverageBuyin(buyinSum, playerCount),
                dateString
                );
        }

        public IList<Cashgame> CreateList(IEnumerable<RawCashgameWithResults> rawGames)
        {
            return rawGames.Select(Create).ToList();
        }

        private IEnumerable<RawCashgameResult> GetRawResults(IEnumerable<RawCheckpoint> rawCheckpoints)
        {
            var results = new List<RawCashgameResult>();
            RawCashgameResult currentResult = null;
            var currentPlayerId = -1;
            foreach (var rawCheckpoint in rawCheckpoints)
            {
                if (currentResult == null || rawCheckpoint.PlayerId != currentPlayerId)
                {
                    currentResult = new RawCashgameResult(rawCheckpoint.PlayerId);
                    currentPlayerId = currentResult.PlayerId;
                    results.Add(currentResult);
                }
                currentResult.AddCheckpoint(rawCheckpoint);
            }
            return results;
        }

        private CashgameResult GetResultFromRawResult(RawCashgameResult rawResult)
        {
            var checkpoints = rawResult.Checkpoints.Select(o => _checkpointFactory.Create(o)).ToList();
            return _cashgameResultFactory.Create(rawResult.PlayerId, checkpoints);
        }

        private DateTime? GetStartTime(IEnumerable<CashgameResult> results)
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

        private DateTime? GetEndTime(IEnumerable<CashgameResult> results)
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

        private int GetBuyinSum(IEnumerable<CashgameResult> results)
        {
            return results.Sum(result => result.Buyin);
        }

        private int GetCashoutSum(IEnumerable<CashgameResult> results)
        {
            return results.Sum(result => result.Stack);
        }

        private bool HasActivePlayers(IEnumerable<CashgameResult> results)
        {
            return results.Any(result => !result.CashoutTime.HasValue);
        }

        private int GetTotalStacks(IEnumerable<CashgameResult> results)
        {
            return results.Sum(result => result.Stack);
        }

        private int GetAverageBuyin(int turnover, int playerCount)
        {
            if (playerCount == 0)
            {
                return 0;
            }
            return (int) Math.Round(turnover/(double) playerCount);
        }

    }

}