using Core.Classes;
using Infrastructure.Data.Classes;

namespace Infrastructure.Data.Mappers
{
    public interface IUserDataMapper
    {
        User Map(RawUser rawUser);
    }
}