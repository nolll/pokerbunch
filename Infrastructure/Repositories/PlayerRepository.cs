using System.Collections.Generic;
using Core.Classes;
using Core.Repositories;
using Infrastructure.Data.Storage.Interfaces;
using Infrastructure.Factories;
using System.Linq;

namespace Infrastructure.Repositories {

	public class PlayerRepository : IPlayerRepository
    {
	    private readonly IPlayerStorage _playerStorage;
	    private readonly IPlayerFactory _playerFactory;

	    public PlayerRepository(
            IPlayerStorage playerStorage,
            IPlayerFactory playerFactory)
	    {
	        _playerStorage = playerStorage;
	        _playerFactory = playerFactory;
	    }

	    public List<Player> GetAll(Homegame homegame){
			return _playerStorage.GetPlayers(homegame.Id).Select(_playerFactory.Create).ToList();
		}

		public Player GetPlayerById(Homegame homegame, int id){
            var rawPlayer = _playerStorage.GetPlayerById(homegame.Id, id);
			return _playerFactory.Create(rawPlayer);
		}

		public Player GetByName(Homegame homegame, string name){
			var rawPlayer = _playerStorage.GetPlayerByName(homegame.Id, name);
            return _playerFactory.Create(rawPlayer);
		}

		public Player GetByUserName(Homegame homegame, string userName){
            var rawPlayer = _playerStorage.GetPlayerByUserName(homegame.Id, userName);
            return _playerFactory.Create(rawPlayer);
		}

		public int AddPlayer(Homegame homegame, string playerName){
			return _playerStorage.AddPlayer(homegame.Id, playerName);
		}

		public int AddPlayerWithUser(Homegame homegame, User user, Role role){
			return _playerStorage.AddPlayerWithUser(homegame.Id, user.Id, (int)role);
		}

		public bool JoinHomegame(Player player, Homegame homegame, User user){
			return _playerStorage.JoinHomegame(player.Id, (int)player.Role, homegame.Id, user.Id);
		}

		public bool DeletePlayer(Player player){
			return _playerStorage.DeletePlayer(player.Id);
		}

	}

}