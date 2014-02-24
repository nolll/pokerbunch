using Core.Classes;
using Infrastructure.Data.Classes;

namespace Infrastructure.Data.Mappers
{
    public interface ITwitterCredentialsDataMapper
    {
        TwitterCredentials Map(RawTwitterCredentials rawCredentials);
    }
}