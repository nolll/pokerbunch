using Core.Classes;
using Infrastructure.Data.Classes;
using Infrastructure.Data.Storage;

namespace Infrastructure.Data.Factories
{
    internal interface IRawHomegameFactory
    {
        RawHomegame Create(StorageDataReader reader);
        RawHomegame Create(Homegame homegame);
    }
}