using Core.Classes;
using Infrastructure.Data.Classes;

namespace Infrastructure.Factories
{
    internal interface IHomegameFactory
    {
        Homegame Create(RawHomegame rawHomegame);
    }
}