using Application.Services;
using Core.Entities;
using Core.Repositories;

namespace Infrastructure.Integration.Social{

	public class SocialServiceProvider : ISocialServiceProvider
    {
	    private readonly ITwitterRepository _twitterRepository;
	    private readonly ISettings _settings;

	    public SocialServiceProvider(
            ITwitterRepository twitterRepository,
            ISettings settings)
	    {
	        _twitterRepository = twitterRepository;
	        _settings = settings;
	    }

	    public ISocialService Get(string identifier){
			if(identifier == SocialServiceIdentifier.Twitter){
				return new TwitterIntegration(
                    _twitterRepository,
                    _settings);
			}
			return null;
		}

	}

}