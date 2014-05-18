using Core.Entities;
using Infrastructure.Data.Classes;
using Infrastructure.Data.Interfaces;

namespace Infrastructure.Data.Factories
{
    public interface IRawHomegameFactory
    {
        RawHomegame Create(IStorageDataReader reader);
        RawHomegame Create(Homegame homegame);
    }
}