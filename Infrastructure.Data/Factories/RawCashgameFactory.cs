using System;
using System.Collections.Generic;
using Application.Services;
using Core.Classes;
using Infrastructure.Data.Classes;
using Infrastructure.Data.Factories.Interfaces;
using Infrastructure.Data.Interfaces;

namespace Infrastructure.Data.Factories
{
    public class RawCashgameFactory : IRawCashgameFactory
    {
        private readonly ITimeProvider _timeProvider;

        public RawCashgameFactory(ITimeProvider timeProvider)
        {
            _timeProvider = timeProvider;
        }

        public RawCashgameWithResults Create(IStorageDataReader reader)
        {
            var location = reader.GetStringValue("Location");
            if (location == "")
            {
                location = null;
            }

            return new RawCashgameWithResults
                {
                    Id = reader.GetIntValue("GameID"),
                    Location = location,
                    Status = reader.GetIntValue("Status"),
                    Date = TimeZoneInfo.ConvertTimeToUtc(reader.GetDateTimeValue("Date")),
                    Results = new List<RawCashgameResult>()
                };
        }

        public RawCashgameWithResults Create(Cashgame cashgame, GameStatus? status = null)
        {
            return new RawCashgameWithResults
            {
                Id = cashgame.Id,
                Location = cashgame.Location,
                Status = status.HasValue ? (int)status.Value : (int)cashgame.Status,
                Date = cashgame.StartTime.HasValue ? cashgame.StartTime.Value : _timeProvider.GetTime(),
                Results = new List<RawCashgameResult>()
            };
        }
    }
}