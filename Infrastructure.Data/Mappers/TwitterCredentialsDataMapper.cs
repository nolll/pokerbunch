using Application.Factories;
using Core.Entities;
using Infrastructure.Data.Classes;

namespace Infrastructure.Data.Mappers
{
    public static class TwitterCredentialsDataMapper
    {
        public static TwitterCredentials Map(RawTwitterCredentials rawCredentials)
        {
            return TwitterCredentialsFactory.Create(
                rawCredentials.Key,
                rawCredentials.Secret,
                rawCredentials.TwitterName);
        }
    }
}