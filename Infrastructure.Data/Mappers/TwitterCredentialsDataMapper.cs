using Application.Factories;
using Core.Entities;
using Infrastructure.Data.Classes;

namespace Infrastructure.Data.Mappers
{
    public class TwitterCredentialsDataMapper : ITwitterCredentialsDataMapper
    {
        private readonly ITwitterCredentialsFactory _twitterCredentialsFactory;

        public TwitterCredentialsDataMapper(ITwitterCredentialsFactory twitterCredentialsFactory)
        {
            _twitterCredentialsFactory = twitterCredentialsFactory;
        }

        public TwitterCredentials Map(RawTwitterCredentials rawCredentials)
        {
            return _twitterCredentialsFactory.Create(
                rawCredentials.Key,
                rawCredentials.Secret,
                rawCredentials.TwitterName);
        }
    }
}