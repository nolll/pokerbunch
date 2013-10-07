using System;
using Core.Classes.Checkpoints;
using Infrastructure.Data.Classes;
using Infrastructure.Data.Storage;

namespace Infrastructure.Data.Factories
{
    public class RawCheckpointFactory : IRawCheckpointFactory
    {
        public RawCheckpoint Create(StorageDataReader reader, TimeZoneInfo timeZone)
        {
            return new RawCheckpoint
                {
                    Id = reader.GetInt("CheckpointID"),
                    Type = reader.GetInt("Type"),
                    Amount = reader.GetInt("Amount"),
                    Stack = reader.GetInt("Stack"),
                    Timestamp = reader.GetDateTime("TimeStamp")
                };
            //var timestamp = TimeZoneInfo.ConvertTime(reader.GetDateTime("TimeStamp"), timeZone);
        }

        public RawCheckpoint Create(Checkpoint checkpoint)
        {
            throw new global::System.NotImplementedException();
        }
    }
}