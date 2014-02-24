using System;
using Core.Classes;
using Core.Classes.Checkpoints;
using Infrastructure.Data.Classes;
using Infrastructure.Data.Interfaces;

namespace Infrastructure.Data.Factories
{
    public class RawCheckpointFactory : IRawCheckpointFactory
    {
        public RawCheckpoint Create(IStorageDataReader reader)
        {
            return new RawCheckpoint
                {
                    GameId = reader.GetIntValue("GameID"),
                    PlayerId = reader.GetIntValue("PlayerID"),
                    Id = reader.GetIntValue("CheckpointID"),
                    Type = reader.GetIntValue("Type"),
                    Amount = reader.GetIntValue("Amount"),
                    Stack = reader.GetIntValue("Stack"),
                    Timestamp = TimeZoneInfo.ConvertTimeToUtc(reader.GetDateTimeValue("TimeStamp"))
                };
        }

        public RawCheckpoint Create(Cashgame cashgame, Checkpoint checkpoint)
        {
            return new RawCheckpoint
            {
                GameId = cashgame.Id,
                Id = checkpoint.Id,
                Type = (int)checkpoint.Type,
                Amount = checkpoint.Amount,
                Stack = checkpoint.Stack,
                Timestamp = checkpoint.Timestamp
            };
        }
    }
}