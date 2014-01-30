using System;
using System.Collections.Generic;
using Application.Services.Interfaces;
using Core.Classes;
using Infrastructure.Data.Classes;
using Infrastructure.Data.Storage;

namespace Infrastructure.Data.Factories
{
    public class RawCashgameFactory : IRawCashgameFactory
    {
        private readonly ITimeProvider _timeProvider;

        public RawCashgameFactory(ITimeProvider timeProvider)
        {
            _timeProvider = timeProvider;
        }

        public RawCashgameWithResults Create(StorageDataReader reader)
        {
            var location = reader.GetString("Location");
            if (location == "")
            {
                location = null;
            }

            return new RawCashgameWithResults
                {
                    Id = reader.GetInt("GameID"),
                    Location = location,
                    Status = reader.GetInt("Status"),
                    Date = TimeZoneInfo.ConvertTimeToUtc(reader.GetDateTime("Date")),
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