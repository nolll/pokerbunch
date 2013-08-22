using System.Collections.Generic;
using Core.Classes;
using Infrastructure.Data.Classes;

namespace Infrastructure.Factories
{
    public interface IHomegameFactory
    {
        Homegame Create(RawHomegame rawHomegame);
        IList<Homegame> CreateList(IEnumerable<RawHomegame> rawHomegames);
    }
}