using System;
using System.Collections.Generic;
using System.Linq;
using Core.Entities;
using Core.Entities.Checkpoints;
using Core.Repositories;
using Infrastructure.Storage.Classes;
using Infrastructure.Storage.Interfaces;

namespace Infrastructure.Storage.Repositories
{
	public class SqlCashgameRepository : ICashgameRepository
    {
	    private readonly ICashgameStorage _cashgameStorage;
	    private readonly ICheckpointStorage _checkpointStorage;

	    public SqlCashgameRepository(
            ICashgameStorage cashgameStorage,
            ICheckpointStorage checkpointStorage)
	    {
	        _cashgameStorage = cashgameStorage;
	        _checkpointStorage = checkpointStorage;
	    }

        public IList<Cashgame> GetFinished(int bunchId, int? year = null)
        {
            var ids = _cashgameStorage.GetGameIds(bunchId, (int?) GameStatus.Finished, year);
            return GetList(ids);
        }

	    public IList<Cashgame> GetByEvent(int eventId)
	    {
            var ids = GetIdsByEvent(eventId);
            return GetList(ids);
	    } 

        public Cashgame GetRunning(int bunchId)
        {
            var id = GetIdByRunning(bunchId);
            return id.HasValue ? GetById(id.Value) : null;
        }
        
        public Cashgame GetById(int cashgameId)
        {
            var rawGame = _cashgameStorage.GetGame(cashgameId);
            var rawCheckpoints = _checkpointStorage.GetCheckpoints(cashgameId);
            var checkpoints = CreateCheckpoints(rawCheckpoints);
            var cashgame = CreateCashgame(rawGame);
            cashgame.AddCheckpoints(checkpoints);
            return cashgame;
        }

        private int? GetIdByRunning(int bunchId)
        {
            return _cashgameStorage.GetRunningCashgameId(bunchId);
        }

        public IList<int> GetYears(int bunchId)
        {
            return _cashgameStorage.GetYears(bunchId);
        }

        private IList<Cashgame> GetList(IList<int> ids)
        {
            var rawCashgames = _cashgameStorage.GetGames(ids);
            var rawCheckpoints = _checkpointStorage.GetCheckpoints(ids);
            return CreateCashgameList(rawCashgames, rawCheckpoints);
        }
        
        private IList<int> GetIdsByEvent(int eventId)
        {
            return _cashgameStorage.GetGameIdsByEvent(eventId);
        }

        public IList<string> GetLocations(int bunchId)
        {
			return _cashgameStorage.GetLocations(bunchId);
		}

		public bool DeleteGame(Cashgame cashgame){
			return _cashgameStorage.DeleteGame(cashgame.Id);
		}

		public int AddGame(Bunch bunch, Cashgame cashgame)
		{
		    var rawCashgame = CreateRawCashgame(cashgame);
            return _cashgameStorage.AddGame(bunch, rawCashgame);
		}

		public bool UpdateGame(Cashgame cashgame)
        {
            var rawCashgame = CreateRawCashgame(cashgame);
            return _cashgameStorage.UpdateGame(rawCashgame);
		}

		public bool EndGame(Bunch bunch, Cashgame cashgame)
        {
            var rawCashgame = CreateRawCashgame(cashgame, GameStatus.Finished);
            return _cashgameStorage.UpdateGame(rawCashgame);
		}

		public bool HasPlayed(int playerId)
        {
			return _cashgameStorage.HasPlayed(playerId);
		}

	    private RawCashgame CreateRawCashgame(Cashgame cashgame, GameStatus? status = null)
	    {
	        var rawStatus = status.HasValue ? (int) status.Value : (int) cashgame.Status;
	        var date = cashgame.StartTime.HasValue ? cashgame.StartTime.Value : DateTime.UtcNow;
            
            return new RawCashgame(cashgame.Id, cashgame.BunchId, cashgame.Location, rawStatus, date);
        }

	    private static Cashgame CreateCashgame(RawCashgame rawGame)
	    {
            return new Cashgame(rawGame.BunchId, rawGame.Location, (GameStatus)rawGame.Status, rawGame.Id);
        }

        private static IList<Checkpoint> CreateCheckpoints(IEnumerable<RawCheckpoint> checkpoints)
	    {
            return checkpoints.Select(RawCheckpoint.CreateReal).ToList();
	    } 

	    private static IList<Cashgame> CreateCashgameList(IEnumerable<RawCashgame> rawGames, IEnumerable<RawCheckpoint> rawCheckpoints)
        {
            var checkpointMap = GetGameCheckpointMap(rawCheckpoints);

            var cashgames = new List<Cashgame>();
            foreach (var rawGame in rawGames)
            {
                IList<RawCheckpoint> gameCheckpoints;
                if (!checkpointMap.TryGetValue(rawGame.Id, out gameCheckpoints))
                {
                    continue;
                }
                var checkpoints = CreateCheckpoints(gameCheckpoints);
                var cashgame = CreateCashgame(rawGame);
                cashgame.AddCheckpoints(checkpoints);
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
                if (!checkpointMap.TryGetValue(checkpoint.CashgameId, out checkpointList))
                {
                    checkpointList = new List<RawCheckpoint>();
                    checkpointMap.Add(checkpoint.CashgameId, checkpointList);
                }
                checkpointList.Add(checkpoint);
            }
            return checkpointMap;
        }
	}
}