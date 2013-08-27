using System.Collections.Generic;
using Core.Classes;
using Core.Repositories;
using Infrastructure.Data.Storage.Interfaces;

namespace Infrastructure.Repositories {

	public class PlayerRepository : IPlayerRepository{
	    private readonly IPlayerStorage _playerStorage;

	    public PlayerRepository(IPlayerStorage playerStorage)
	    {
	        _playerStorage = playerStorage;
	    }

		public List<Player> GetAll(Homegame homegame){
			return _playerStorage.GetPlayers(homegame);
		}

		public Player GetPlayerById(Homegame homegame, int id){
			return _playerStorage.GetPlayerById(homegame, id);
		}

		public Player GetByName(Homegame homegame, string name){
			return _playerStorage.GetPlayerByName(homegame, name);
		}

		public Player GetByUserName(Homegame homegame, string userName){
			return _playerStorage.GetPlayerByUserName(homegame, userName);
		}

		public int AddPlayer(Homegame homegame, string playerName){
			return _playerStorage.AddPlayer(homegame, playerName);
		}

		public int AddPlayerWithUser(Homegame homegame, User user, Role role){
			return _playerStorage.AddPlayerWithUser(homegame, user, (int)role);
		}

		public bool JoinHomegame(Player player, Homegame homegame, User user){
			return _playerStorage.JoinHomegame(player, homegame, user);
		}

		public bool DeletePlayer(Player player){
			return _playerStorage.DeletePlayer(player);
		}

	}

}