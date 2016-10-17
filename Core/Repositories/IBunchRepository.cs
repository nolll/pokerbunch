using System.Collections.Generic;
using Core.Entities;

namespace Core.Repositories
{
    public interface IBunchRepository
    {
        Bunch Get(string id);
        IList<SmallBunch> List();
        IList<SmallBunch> List(string userName);
        Bunch Add(Bunch bunch);
        Bunch Update(Bunch bunch);
    }
}