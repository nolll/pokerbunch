using System.Collections.Generic;
using System.Linq;
using Application.Factories;
using Core.Classes;
using Core.Classes.Checkpoints;
using Infrastructure.Data.Classes;
using Infrastructure.Data.Factories.Interfaces;

namespace Infrastructure.Data.Mappers
{
    public class CashgameDataMapper : ICashgameDataMapper
    {
        private readonly ICashgameResultFactory _cashgameResultFactory;
        private readonly ICheckpointFactory _checkpointFactory;
        private readonly ICashgameFactory _cashgameFactory;

        public CashgameDataMapper(
            ICashgameResultFactory cashgameResultFactory,
            ICheckpointFactory checkpointFactory,
            ICashgameFactory cashgameFactory)
        {
            _cashgameResultFactory = cashgameResultFactory;
            _checkpointFactory = checkpointFactory;
            _cashgameFactory = cashgameFactory;
        }

        public Cashgame Map(RawCashgame rawGame, IEnumerable<RawCheckpoint> checkpoints)
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
                    realCheckpoints.Add(_checkpointFactory.Create(playerCheckpoint));
                }
                var playerResults = _cashgameResultFactory.Create(playerKey, realCheckpoints);
                results.Add(playerResults);
            }

            return _cashgameFactory.Create(rawGame.Location, rawGame.Status, rawGame.Id, results);
        }

        public IList<Cashgame> MapList(IEnumerable<RawCashgame> rawGames, IEnumerable<RawCheckpoint> checkpoints)
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

        private IDictionary<int, IList<RawCheckpoint>> GetGameCheckpointMap(IEnumerable<RawCheckpoint> checkpoints)
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