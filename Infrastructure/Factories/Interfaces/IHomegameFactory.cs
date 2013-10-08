using Core.Classes;
using Infrastructure.Data.Classes;

namespace Infrastructure.Factories
{
    public interface IHomegameFactory
    {
        Homegame Create(RawHomegame rawHomegame);
    }
}