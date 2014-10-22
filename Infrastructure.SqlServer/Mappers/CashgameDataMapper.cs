using System.Collections.Generic;
using System.Linq;
using Core.Entities;
using Core.Entities.Checkpoints;
using Infrastructure.Storage;

namespace Infrastructure.SqlServer.Mappers
{
    public static class CashgameDataMapper
    {
        public static Cashgame Map(RawCashgame rawGame, IEnumerable<RawCheckpoint> checkpoints)
        {
            var playerCheckpointMap = new Dictionary<int, IList<RawCheckpoint>>();
            foreach (var checkpoint in checkpoints)
            {
                IList<RawCheckpoint> checkpointList;
                if (!playerCheckpointMap.TryGetValue(checkpoint.PlayerId, out checkpointList))
                {
                    checkpointList = new List<RawCheckpoint>();
                    playerCheckpointMap.Add(checkpoint.PlayerId, checkpointList);
                }
                checkpointList.Add(checkpoint);
            }

            var results = new List<CashgameResult>();
            foreach (var playerKey in playerCheckpointMap.Keys)
            {
                var playerCheckpoints = playerCheckpointMap[playerKey].OrderBy(o => o.Timestamp);
                var realCheckpoints = new List<Checkpoint>();
                foreach (var playerCheckpoint in playerCheckpoints)
                {
                    realCheckpoints.Add(CheckpointDataMapper.Map(playerCheckpoint));
                }
                var playerResults = new CashgameResult(playerKey, realCheckpoints);
                results.Add(playerResults);
            }

            return new Cashgame(rawGame.BunchId, rawGame.Location, (GameStatus)rawGame.Status, rawGame.Id, results);
        }

        public static IList<Cashgame> MapList(IEnumerable<RawCashgame> rawGames, IEnumerable<RawCheckpoint> checkpoints)
        {
            var checkpointMap = GetGameCheckpointMap(checkpoints);

            var cashgames = new List<Cashgame>();
            foreach (var rawGame in rawGames)
            {
                IList<RawCheckpoint> gameCheckpoints;
                if (!checkpointMap.TryGetValue(rawGame.Id, out gameCheckpoints))
                {
                    continue;
                }
                var cashgame = Map(rawGame, gameCheckpoints);
                cashgames.Add(cashgame);
            }
            return cashgames;
        }

        private static IDictionary<int, IList<RawCheckpoint>> GetGameCheckpointMap(IEnumerable<RawCheckpoint> checkpoints)
        {
            var checkpointMap = new Dictionary<int, IList<RawCheckpoint>>();
            foreach (var checkpoint in checkpoints)
            {
                IList<RawCheckpoint> checkpointList;
                if (!checkpointMap.TryGetValue(checkpoint.GameId, out checkpointList))
                {
                    checkpointList = new List<RawCheckpoint>();
                    checkpointMap.Add(checkpoint.GameId, checkpointList);
                }
                checkpointList.Add(checkpoint);
            }
            return checkpointMap;
        }

        
    }
}