using Core.Classes;
using Infrastructure.Data.Classes;

namespace Infrastructure.Factories
{
    public interface ITwitterCredentialsFactory
    {
        TwitterCredentials Create(RawTwitterCredentials rawCredentials);
    }
}