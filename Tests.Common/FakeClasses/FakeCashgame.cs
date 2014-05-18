using System;
using System.Collections.Generic;
using Core.Entities;

namespace Tests.Common.FakeClasses
{
    public class FakeCashgame : Cashgame
    {
        public FakeCashgame(
            int id = default(int),
            string location = default(string),
            GameStatus status = default(GameStatus),
            bool isStarted = default(bool),
            DateTime? startTime = default(DateTime?),
            DateTime? endTime = default(DateTime?),
            int duration = default(int),
            IList<CashgameResult> results = default(IList<CashgameResult>),
            int playerCount = default(int),
            int diff = default(int),
            int turnover = default(int),
            bool hasActivePlayers = default(bool),
            int totalStacks = default(int),
            int averageBuyin = default(int),
            string dateString = default(string)
            ) : base(
                id,
                location,
                status,
                isStarted,
                startTime,
                endTime,
                duration,
                results ?? new List<CashgameResult>(),
                playerCount,
                diff,
                turnover,
                hasActivePlayers,
                totalStacks,
                averageBuyin,
                dateString
                )
        {
        }

    }
}