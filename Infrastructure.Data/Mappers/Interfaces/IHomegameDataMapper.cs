using Core.Classes;
using Infrastructure.Data.Classes;

namespace Infrastructure.Data.Mappers
{
    public interface IHomegameDataMapper
    {
        Homegame Map(RawHomegame rawHomegame);
    }
}