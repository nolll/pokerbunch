using System;
using System.Configuration;
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

	    public TwitterIntegration(ITwitterRepository twitterRepository)
	    {
	        _twitterRepository = twitterRepository;
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

            return new TwitterCredentials(
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
            return ConfigurationManager.AppSettings.Get("TwitterKey");
	    }

	    private string GetSecret()
        {
            return ConfigurationManager.AppSettings.Get("TwitterSecret");
        }

        public void ShareResult(User user, int amount)
        {
            var message = GetMessage(amount);
            PostToTwitter(user, message);
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