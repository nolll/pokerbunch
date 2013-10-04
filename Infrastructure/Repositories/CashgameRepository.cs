using System;
using System.Collections.Generic;
using Core.Classes;
using Core.Classes.Checkpoints;
using Core.Repositories;
using Infrastructure.Data.Classes;
using Infrastructure.Data.Storage.Interfaces;
using Infrastructure.Factories;
using Infrastructure.System;

namespace Infrastructure.Repositories {
	
	public class CashgameRepository : ICashgameRepository{

	    private readonly ICashgameStorage _cashgameStorage;
	    private readonly ICashgameFactory _cashgameFactory;
	    private readonly IPlayerRepository _playerRepository;
	    private readonly ITimeProvider _timeProvider;
	    private readonly ICashgameSuiteFactory _cashgameSuiteFactory;
	    private readonly ICashgameResultFactory _cashgameResultFactory;
	    private readonly ICheckpointRepository _checkpointRepository;

	    public CashgameRepository(
            ICashgameStorage cashgameStorage,
			ICashgameFactory cashgameFactory,
            IPlayerRepository playerRepository,
			ITimeProvider timeProvider,
			ICashgameSuiteFactory cashgameSuiteFactory,
			ICashgameResultFactory cashgameResultFactory,
            ICheckpointRepository checkpointRepository)
	    {
	        _cashgameStorage = cashgameStorage;
	        _cashgameFactory = cashgameFactory;
	        _playerRepository = playerRepository;
	        _timeProvider = timeProvider;
	        _cashgameSuiteFactory = cashgameSuiteFactory;
	        _cashgameResultFactory = cashgameResultFactory;
	        _checkpointRepository = checkpointRepository;
	    }

	    public IList<Cashgame> GetPublished(Homegame homegame, int? year = null){
			return GetGames(homegame, GameStatus.Published, year);
		}

		public Cashgame GetRunning(Homegame homegame){
			var games = GetGames(homegame, GameStatus.Running, null);
			if(games.Count == 0){
				return null;
			}
			return games[0];
		}

        public IList<Cashgame> GetAll(Homegame homegame, int? year = null)
        {
			return GetGames(homegame, null, year);
		}

		public Cashgame GetByDate(Homegame homegame, DateTime date){
			var rawGame = _cashgameStorage.GetGame(homegame, date);
			var players = _playerRepository.GetAll(homegame);
			return GetGameFromRawGame(rawGame, players);
		}

		public Cashgame GetByDateString(Homegame homegame, string dateString){
			var date = DateTimeFactory.Create(dateString, homegame.Timezone);
			return GetByDate(homegame, date);
		}

		public CashgameSuite GetSuite(Homegame homegame, int? year = null){
			var players = _playerRepository.GetAll(homegame);
			var cashgames = GetPublished(homegame, year);
			return _cashgameSuiteFactory.Create(cashgames, players);
		}

		public IList<int> GetYears(Homegame homegame){
			return _cashgameStorage.GetYears(homegame.Slug);
		}

		private IList<Cashgame> GetGames(Homegame homegame, GameStatus? status = null, int? year = null){
			var rawGames = _cashgameStorage.GetGames(homegame, status, year);
			var players = _playerRepository.GetAll(homegame);
			return GetGamesFromRawGames(rawGames, players);
		}

		private IList<Cashgame> GetGamesFromRawGames(IEnumerable<RawCashgame> rawGames, List<Player> players){
			var games = new List<Cashgame>();
			foreach(var rawGame in rawGames){
				games.Add(GetGameFromRawGame(rawGame, players));
			}
			return games;
		}

		private Cashgame GetGameFromRawGame(RawCashgame rawGame, List<Player> players){
			var results = new List<CashgameResult>();
			var rawResults = rawGame.Results;
			foreach(var rawResult in rawResults){
				results.Add(GetResultFromRawResult(rawResult, players));
			}
			return _cashgameFactory.Create(rawGame.Location, rawGame.Status, rawGame.Id, results);
		}

		private CashgameResult GetResultFromRawResult(RawCashgameResult rawResult, List<Player> players){
			var player = GetPlayer(players, rawResult.PlayerId);
			var checkpoints = rawResult.Checkpoints;
			return _cashgameResultFactory.Create(player, checkpoints);
		}

		private Player GetPlayer(List<Player> players, int playerId){
			foreach(var player in players){
				if(player.Id == playerId){
					return player;
				}
			}
			return null;
		}

        public IList<string> GetLocations(Homegame homegame)
        {
			return _cashgameStorage.GetLocations(homegame.Slug);
		}

		public bool DeleteGame(Cashgame cashgame){
			return _cashgameStorage.DeleteGame(cashgame.Id);
		}

		public int AddGame(Homegame homegame, Cashgame cashgame)
		{
		    var rawCashgame = GetRawCashgame(cashgame);
			return _cashgameStorage.AddGame(homegame.Id, rawCashgame);
		}

		public void AddCheckpoint(Cashgame cashgame, Player player, Checkpoint checkpoint){
			_checkpointRepository.AddCheckpoint(cashgame, player, checkpoint);
		}

		public void UpdateCheckpoint(Checkpoint checkpoint){
            _checkpointRepository.UpdateCheckpoint(checkpoint);
		}

		public void DeleteCheckpoint(int id){
            _checkpointRepository.DeleteCheckpoint(id);
		}

		public bool UpdateGame(Cashgame cashgame){
			var rawCashgame = GetRawCashgame(cashgame);
			return _cashgameStorage.UpdateGame(rawCashgame);
		}

		public bool StartGame(Cashgame cashgame){
			var rawCashgame = GetRawCashgame(cashgame, GameStatus.Running);
			return _cashgameStorage.UpdateGame(rawCashgame);
		}

		public bool EndGame(Cashgame cashgame){
			var rawCashgame = GetRawCashgame(cashgame, GameStatus.Published);
			return _cashgameStorage.UpdateGame(rawCashgame);
		}

		public bool HasPlayed(Player player){
			return _cashgameStorage.HasPlayed(player.Id);
		}

		private RawCashgame GetRawCashgame(Cashgame cashgame, GameStatus? status = null){
			var id = cashgame.Id;
			var location = cashgame.Location;
			if(!status.HasValue){
				status = cashgame.Status;
			}
			var date = cashgame.StartTime;
			if(!date.HasValue){
				date = _timeProvider.GetTime();
			}
			return new RawCashgame(id, location, (int)status.Value, date.Value);
		}
 
	}

}