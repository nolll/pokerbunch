using System;
using Application.Factories;
using Application.Services;
using Application.Urls;
using Core.Entities;
using Core.Repositories;
using TweetSharp;

namespace Infrastructure.Integration.Social
{
	public class TwitterIntegration : ISocialService, ITwitterIntegration
    {
	    private readonly ITwitterRepository _twitterRepository;
	    private readonly ISettings _settings;
	    private readonly IUrlProvider _urlProvider;

	    public TwitterIntegration(
            ITwitterRepository twitterRepository,
            ISettings settings,
            IUrlProvider urlProvider)
	    {
	        _twitterRepository = twitterRepository;
	        _settings = settings;
	        _urlProvider = urlProvider;
	    }

        public string GetAuthUrl()
        {
			var service = GetService();
            var requestToken = service.GetRequestToken(new TwitterCallbackUrl().Absolute);
            return service.GetAuthenticationUrl(requestToken).ToString();
		}

        public TwitterCredentials GetCredentials(string token, string verifier)
        {
            var requestToken = new OAuthRequestToken { Token = token };

            var key = GetKey();
            var secret = GetSecret();
            var service = new TwitterService(key, secret);

            //todo: There seems to be an error here. The AccessToken just contains question marks
            var accessToken = service.GetAccessToken(requestToken, verifier);

            service.AuthenticateWith(accessToken.Token, accessToken.TokenSecret);
            var twitterUser = service.VerifyCredentials(new VerifyCredentialsOptions());

            return TwitterCredentialsFactory.Create(
                    accessToken.Token,
                    accessToken.TokenSecret,
                    twitterUser.ScreenName);
        }

        private TwitterService GetService()
        {
            return new TwitterService(GetKey(), GetSecret());
        }
        
	    private string GetKey()
        {
            return _settings.GetTwitterKey();
        }

	    private string GetSecret()
        {
            return _settings.GetTwitterSecret();
        }

        public void ShareResult(User user, int amount)
        {
            var message = GetMessage(amount);
            PostToTwitter(user, message);
        }

        private string GetMessage(int amount)
        {
            var formattedAmount = Math.Abs(amount) + " kr";
            var wonOrLost = amount < 0 ? "lost" : "won";
            return string.Format("I just {0} {1} playing poker. #pokerbunch", wonOrLost, formattedAmount);
        }

        private void PostToTwitter(User user, string message)
        {
            var credentials = _twitterRepository.GetCredentials(user);
            if (credentials != null)
            {
                var connection = new TwitterService(GetKey(), GetSecret(), credentials.Key, credentials.Secret);
                connection.SendTweet(new SendTweetOptions { Status = message });
            }
        }

	}

}