using Core.Classes;
using Infrastructure.Data.Classes;

namespace Infrastructure.Data.Factories.Interfaces
{
    public interface ITwitterCredentialsFactory
    {
        TwitterCredentials Create(RawTwitterCredentials rawCredentials);
    }
}