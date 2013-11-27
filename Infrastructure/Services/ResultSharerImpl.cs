using Core.Classes;
using Core.Repositories;
using Core.Services;

namespace Infrastructure.Services{

	public class ResultSharerImpl : IResultSharer
    {
	    private readonly ISharingRepository _sharingRepository;
	    private readonly IUserRepository _userRepository;
	    private readonly ISocialServiceProvider _socialServiceFactory;

	    public ResultSharerImpl(
            ISharingRepository sharingRepository,
			IUserRepository userRepository,
			ISocialServiceProvider socialServiceFactory)
	    {
	        _sharingRepository = sharingRepository;
	        _userRepository = userRepository;
	        _socialServiceFactory = socialServiceFactory;
	    }

	    public void ShareResult(Cashgame cashgame){
			foreach(var result in cashgame.Results){
				ShareSingleResult(result);
			}
		}

		private void ShareSingleResult(CashgameResult result){
			var user = GetUser(result);
			var services = _sharingRepository.GetServices(user);
			foreach(var service in services){
				ShareToService(service, user, result);
			}
		}

		public void ShareToService(string serviceName, User user, CashgameResult result){
			var service = _socialServiceFactory.Get(serviceName);
			service.ShareResult(user, result.Winnings);
		}

		private User GetUser(CashgameResult result){
			return _userRepository.GetById(result.Player.UserId);
		}

	}

}