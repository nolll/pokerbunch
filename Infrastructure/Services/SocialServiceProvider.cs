using Core.Classes;
using Core.Services;
using Infrastructure.Data.Storage.Interfaces;
using Infrastructure.Integration.Twitter;

namespace Infrastructure.Services{

	public class SocialServiceProvider : ISocialServiceProvider
    {
	    private readonly ITwitterStorage _twitterStorage;
	    private readonly ISettings _settings;
	    private readonly IUrlProvider _urlProvider;

	    public SocialServiceProvider(
            ITwitterStorage twitterStorage,
            ISettings settings,
            IUrlProvider urlProvider)
	    {
	        _twitterStorage = twitterStorage;
	        _settings = settings;
	        _urlProvider = urlProvider;
	    }

	    public ISocialService Get(string identifier){
			if(identifier == SocialServiceIdentifier.Twitter){
				return new TwitterIntegration(_twitterStorage, _settings, _urlProvider);
			}
			return null;
		}

	}

}