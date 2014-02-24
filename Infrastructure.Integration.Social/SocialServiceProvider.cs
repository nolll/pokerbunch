using Application.Factories;
using Application.Services;
using Core.Classes;
using Core.Repositories;

namespace Infrastructure.Integration.Social{

	public class SocialServiceProvider : ISocialServiceProvider
    {
	    private readonly ITwitterRepository _twitterRepository;
	    private readonly ISettings _settings;
	    private readonly IUrlProvider _urlProvider;
	    private readonly ITwitterCredentialsFactory _twitterCredentialsFactory;

	    public SocialServiceProvider(
            ITwitterRepository twitterRepository,
            ISettings settings,
            IUrlProvider urlProvider,
            ITwitterCredentialsFactory twitterCredentialsFactory)
	    {
	        _twitterRepository = twitterRepository;
	        _settings = settings;
	        _urlProvider = urlProvider;
	        _twitterCredentialsFactory = twitterCredentialsFactory;
	    }

	    public ISocialService Get(string identifier){
			if(identifier == SocialServiceIdentifier.Twitter){
				return new TwitterIntegration(
                    _twitterRepository,
                    _settings,
                    _urlProvider,
                    _twitterCredentialsFactory);
			}
			return null;
		}

	}

}