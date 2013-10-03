using Core.Classes;
using Core.Services;
using Infrastructure.Data.Storage.Interfaces;

namespace Infrastructure.Services{

	public class ResultSharerImpl : IResultSharer
    {
	    private readonly ISharingStorage _sharingStorage;
	    private readonly IUserStorage _userStorage;
	    private readonly ISocialServiceProvider _socialServiceFactory;

	    public ResultSharerImpl(
            ISharingStorage sharingStorage,
			IUserStorage userStorage,
			ISocialServiceProvider socialServiceFactory)
	    {
	        _sharingStorage = sharingStorage;
	        _userStorage = userStorage;
	        _socialServiceFactory = socialServiceFactory;
	    }

	    public void ShareResult(Cashgame cashgame){
			foreach(var result in cashgame.Results){
				ShareSingleResult(result);
			}
		}

		private void ShareSingleResult(CashgameResult result){
			var user = GetUser(result);
			var services = _sharingStorage.GetServices(user);
			foreach(var service in services){
				ShareToService(service, user, result);
			}
		}

		public void ShareToService(string serviceName, User user, CashgameResult result){
			var service = _socialServiceFactory.Get(serviceName);
			service.ShareResult(user, result.Winnings);
		}

		private User GetUser(CashgameResult result){
			return _userStorage.GetUserByName(result.Player.UserName);
		}

	}

}