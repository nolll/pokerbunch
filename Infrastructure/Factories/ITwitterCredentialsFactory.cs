using Core.Classes;
using Infrastructure.Data.Classes;

namespace Infrastructure.Factories
{
    internal interface ITwitterCredentialsFactory
    {
        TwitterCredentials Create(RawTwitterCredentials rawCredentials);
    }
}