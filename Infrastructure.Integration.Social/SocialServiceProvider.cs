using Application.Services;
using Core.Entities;
using Core.Repositories;

namespace Infrastructure.Integration.Social
{
    public class SocialServiceProvider : ISocialServiceProvider
    {
        private readonly ITwitterRepository _twitterRepository;

        public SocialServiceProvider(ITwitterRepository twitterRepository)
        {
            _twitterRepository = twitterRepository;
        }

        public ISocialService Get(string identifier)
        {
            if (identifier == SocialServiceIdentifier.Twitter)
                return new TwitterIntegration(_twitterRepository);
            return null;
        }
    }
}