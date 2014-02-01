using Core.Classes;
using Infrastructure.Data.Classes;

namespace Infrastructure.Data.Factories.Interfaces
{
    public interface IHomegameFactory
    {
        Homegame Create(RawHomegame rawHomegame);
    }
}