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
	    private readonly IPlayerStorage _playerStorage;
	    private readonly ITimeProvider _timeProvider;
	    private readonly ICashgameSuiteFactory _cashgameSuiteFactory;
	    private readonly ICashgameResultFactory _cashgameResultFactory;

	    public CashgameRepository(
            ICashgameStorage cashgameStorage,
			ICashgameFactory cashgameFactory,
			IPlayerStorage playerStorage,
			ITimeProvider timeProvider,
			ICashgameSuiteFactory cashgameSuiteFactory,
			ICashgameResultFactory cashgameResultFactory)
	    {
	        _cashgameStorage = cashgameStorage;
	        _cashgameFactory = cashgameFactory;
	        _playerStorage = playerStorage;
	        _timeProvider = timeProvider;
	        _cashgameSuiteFactory = cashgameSuiteFactory;
	        _cashgameResultFactory = cashgameResultFactory;
	    }

	    public List<Cashgame> GetPublished(Homegame homegame, int? year = null){
			return getGames(homegame, GameStatus.Published, year);
		}

		public Cashgame GetRunning(Homegame homegame){
			var games = getGames(homegame, GameStatus.Running, null);
			if(games.Count == 0){
				return null;
			}
			return games[0];
		}

		public List<Cashgame> GetAll(Homegame homegame, int? year = null){
			return getGames(homegame, null, year);
		}

		public Cashgame GetByDate(Homegame homegame, DateTime date){
			var rawGame = _cashgameStorage.GetGame(homegame, date);
			var players = _playerStorage.GetPlayers(homegame);
			return getGameFromRawGame(rawGame, players);
		}

		public Cashgame GetByDateString(Homegame homegame, string dateString){
			var date = DateTimeFactory.Create(dateString, homegame.Timezone);
			return GetByDate(homegame, date);
		}

		public CashgameSuite GetSuite(Homegame homegame, int? year = null){
			var players = _playerStorage.GetPlayers(homegame);
			var cashgames = GetPublished(homegame, year);
			return _cashgameSuiteFactory.Create(cashgames, players);
		}

		public List<int> GetYears(Homegame homegame){
			return _cashgameStorage.GetYears(homegame);
		}

		private List<Cashgame> getGames(Homegame homegame, GameStatus? status = null, int? year = null){
			var rawGames = _cashgameStorage.GetGames(homegame, status, year);
			var players = _playerStorage.GetPlayers(homegame);
			return getGamesFromRawGames(rawGames, players);
		}

		private List<Cashgame> getGamesFromRawGames(List<RawCashgame> rawGames, List<Player> players){
			var games = new List<Cashgame>();
			foreach(var rawGame in rawGames){
				games.Add(getGameFromRawGame(rawGame, players));
			}
			return games;
		}

		private Cashgame getGameFromRawGame(RawCashgame rawGame, List<Player> players){
			var results = new List<CashgameResult>();
			var rawResults = rawGame.Results;
			foreach(var rawResult in rawResults){
				results.Add(getResultFromRawResult(rawResult, players));
			}
			return _cashgameFactory.Create(rawGame.Location, rawGame.Status, rawGame.Id, results);
		}

		private CashgameResult getResultFromRawResult(RawCashgameResult rawResult, List<Player> players){
			var player = getPlayer(players, rawResult.PlayerId);
			var checkpoints = rawResult.Checkpoints;
			return _cashgameResultFactory.Create(player, checkpoints);
		}

		private Player getPlayer(List<Player> players, int playerId){
			foreach(var player in players){
				if(player.Id == playerId){
					return player;
				}
			}
			return null;
		}

		public List<string> GetLocations(Homegame homegame){
			return _cashgameStorage.GetLocations(homegame);
		}

		public bool DeleteGame(Cashgame cashgame){
			return _cashgameStorage.DeleteGame(cashgame);
		}

		public int AddGame(Homegame homegame, Cashgame cashgame){
			return _cashgameStorage.AddGame(homegame, cashgame);
		}

		public void AddCheckpoint(Cashgame cashgame, Player player, Checkpoint checkpoint){
			_cashgameStorage.AddCheckpoint(cashgame, player, checkpoint);
		}

		public void UpdateCheckpoint(Checkpoint checkpoint){
			_cashgameStorage.UpdateCheckpoint(checkpoint);
		}

		public void DeleteCheckpoint(int id){
			_cashgameStorage.DeleteCheckpoint(id);
		}

		public bool UpdateGame(Cashgame cashgame){
			var rawCashgame = getRawCashgame(cashgame);
			return _cashgameStorage.UpdateGame(rawCashgame);
		}

		public bool StartGame(Cashgame cashgame){
			var rawCashgame = getRawCashgame(cashgame, GameStatus.Running);
			return _cashgameStorage.UpdateGame(rawCashgame);
		}

		public bool EndGame(Cashgame cashgame){
			var rawCashgame = getRawCashgame(cashgame, GameStatus.Published);
			return _cashgameStorage.UpdateGame(rawCashgame);
		}

		public bool HasPlayed(Player player){
			return _cashgameStorage.HasPlayed(player);
		}

		private RawCashgame getRawCashgame(Cashgame cashgame, GameStatus? status = null){
			var id = cashgame.Id;
			var location = cashgame.Location;
			if(!status.HasValue){
				status = cashgame.Status;
			}
			var date = cashgame.StartTime;
			if(!date.HasValue){
				date = _timeProvider.GetTime();
			}
			return new RawCashgame(id, location, status.Value, date.Value);
		}
 
	}

}