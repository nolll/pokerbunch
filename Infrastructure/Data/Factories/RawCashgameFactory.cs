using System.Collections.Generic;
using Core.Classes;
using Infrastructure.Data.Classes;
using Infrastructure.Data.Storage;
using Infrastructure.System;

namespace Infrastructure.Data.Factories
{
    public class RawCashgameFactory : IRawCashgameFactory
    {
        private readonly ITimeProvider _timeProvider;

        public RawCashgameFactory(ITimeProvider timeProvider)
        {
            _timeProvider = timeProvider;
        }

        public RawCashgame Create(StorageDataReader reader)
        {
            var location = reader.GetString("Location");
            if (location == "")
            {
                location = null;
            }

            return new RawCashgame
                {
                    Id = reader.GetInt("GameID"),
                    Location = location,
                    Status = reader.GetInt("Status"),
                    Date = reader.GetDateTime("Date"),
                    Results = new List<RawCashgameResult>()
                };
        }

        public RawCashgame Create(Cashgame cashgame, GameStatus? status = null)
        {
            return new RawCashgame
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