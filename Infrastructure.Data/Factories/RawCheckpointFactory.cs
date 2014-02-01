using System;
using Core.Classes;
using Core.Classes.Checkpoints;
using Infrastructure.Data.Classes;
using Infrastructure.Data.Factories.Interfaces;
using Infrastructure.Data.Interfaces;

namespace Infrastructure.Data.Factories
{
    public class RawCheckpointFactory : IRawCheckpointFactory
    {
        public RawCheckpoint Create(IStorageDataReader reader)
        {
            return new RawCheckpoint
                {
                    GameId = reader.GetInt("GameID"),
                    PlayerId = reader.GetInt("PlayerID"),
                    Id = reader.GetInt("CheckpointID"),
                    Type = reader.GetInt("Type"),
                    Amount = reader.GetInt("Amount"),
                    Stack = reader.GetInt("Stack"),
                    Timestamp = TimeZoneInfo.ConvertTimeToUtc(reader.GetDateTime("TimeStamp"))
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