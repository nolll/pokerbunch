using Core.Entities;
using Infrastructure.Data.Classes;

namespace Infrastructure.Data.Mappers
{
    public static class TwitterCredentialsDataMapper
    {
        public static TwitterCredentials Map(RawTwitterCredentials rawCredentials)
        {
            return new TwitterCredentials(
                rawCredentials.Key,
                rawCredentials.Secret,
                rawCredentials.TwitterName);
        }
    }
}