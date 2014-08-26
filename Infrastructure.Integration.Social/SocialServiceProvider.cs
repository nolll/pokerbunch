using Application.Factories;
using Application.Services;
using Core.Entities;
using Core.Repositories;

namespace Infrastructure.Integration.Social{

	public class SocialServiceProvider : ISocialServiceProvider
    {
	    private readonly ITwitterRepository _twitterRepository;
	    private readonly ISettings _settings;
	    private readonly IUrlProvider _urlProvider;

	    public SocialServiceProvider(
            ITwitterRepository twitterRepository,
            ISettings settings,
            IUrlProvider urlProvider)
	    {
	        _twitterRepository = twitterRepository;
	        _settings = settings;
	        _urlProvider = urlProvider;
	    }

	    public ISocialService Get(string identifier){
			if(identifier == SocialServiceIdentifier.Twitter){
				return new TwitterIntegration(
                    _twitterRepository,
                    _settings,
                    _urlProvider);
			}
			return null;
		}

	}

}