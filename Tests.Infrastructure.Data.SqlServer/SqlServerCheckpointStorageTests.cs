using System.Collections.Generic;
using Core.Services;
using Infrastructure.Data.Classes;
using Infrastructure.Data.SqlServer;
using NUnit.Framework;

namespace Tests.Infrastructure.Data.SqlServer
{
    public class SqlServerCheckpointStorageTests : StorageTests
    {
        [Test]
        public void AddCheckpoint_CallsStorageWithCorrectSql()
        {
            const int cashgameId = 1;
            const int playerId = 2;
            var checkpoint = new RawCheckpoint
                {
                    Type = 3,
                    Amount = 4,
                    Stack = 5,
                    Timestamp = A.DateTime.AsLocal().Build()
                };
            const string expectedSql = "INSERT INTO cashgamecheckpoint (GameID, PlayerID, Type, Amount, Stack, Timestamp) VALUES (1, 2, 3, 4, 5, '2001-01-01 01:01:01') SELECT SCOPE_IDENTITY() AS [SCOPE_IDENTITY]";

            var sut = GetSut();
            sut.AddCheckpoint(cashgameId, playerId, checkpoint);

            Assert.AreEqual(expectedSql, StorageProvider.Sql);
        }

        [Test]
        public void UpdateCheckpoint_CallsStorageWithCorrectSql()
        {
            var checkpoint = new RawCheckpoint
                {
                    Timestamp = A.DateTime.Build(),
                    Amount = 1,
                    Stack = 2,
                    Id = 3
                };
            const string expectedSql = "UPDATE cashgamecheckpoint SET Timestamp = '2001-01-01 01:01:01', Amount = 1, Stack = 2 WHERE CheckpointID = 3";

            var sut = GetSut();
            sut.UpdateCheckpoint(checkpoint);

            Assert.AreEqual(expectedSql, StorageProvider.Sql);
        }

        [Test]
        public void DeleteCheckpoint_CallsStorageWithCorrectSql()
        {
            const int checkpointId = 1;
            const string expectedSql = "DELETE FROM cashgamecheckpoint WHERE CheckpointID = 1";

            var sut = GetSut();
            sut.DeleteCheckpoint(checkpointId);

            Assert.AreEqual(expectedSql, StorageProvider.Sql);
        }

        [Test]
        public void GetCheckpoints_ForSingleCashgame_CallsStorageWithCorrectSql()
        {
            const int cashgameId = 1;
            const string expectedSql = "SELECT cp.GameID, cp.CheckpointID, cp.PlayerID, cp.Type, cp.Stack, cp.Amount, cp.Timestamp FROM cashgamecheckpoint cp WHERE cp.GameID = 1 ORDER BY cp.PlayerID, cp.Timestamp";

            var sut = GetSut();
            sut.GetCheckpoints(cashgameId);

            Assert.AreEqual(expectedSql, StorageProvider.Sql);
        }

        [Test]
        public void GetCheckpoints_ForCashgameList_CallsStorageWithCorrectSql()
        {
            var cashgameIds = new List<int>{1, 2, 3};
            const string expectedSql = "SELECT cp.GameID, cp.CheckpointID, cp.PlayerID, cp.Type, cp.Stack, cp.Amount, cp.Timestamp FROM cashgamecheckpoint cp WHERE cp.GameID IN (1,2,3) ORDER BY cp.PlayerID, cp.Timestamp";

            var sut = GetSut();
            sut.GetCheckpoints(cashgameIds);

            Assert.AreEqual(expectedSql, StorageProvider.Sql);
        }

        [Test]
        public void GetCheckpoint_CallsStorageWithCorrectSql()
        {
            const int checkpointId = 1;
            const string expectedSql = "SELECT cp.GameID, cp.CheckpointID, cp.PlayerID, cp.Type, cp.Stack, cp.Amount, cp.Timestamp FROM cashgamecheckpoint cp WHERE cp.CheckpointID = 1";

            var sut = GetSut();
            sut.GetCheckpoint(checkpointId);

            Assert.AreEqual(expectedSql, StorageProvider.Sql);
        }

        private SqlServerCheckpointStorage GetSut()
        {
            return new SqlServerCheckpointStorage(
                StorageProvider,
                GetMock<ITimeProvider>().Object);
        }
    }
}
