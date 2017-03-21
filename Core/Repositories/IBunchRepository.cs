using System.Collections.Generic;
using Core.Entities;

namespace Core.Repositories
{
    public interface IBunchRepository
    {
        Bunch Get(string id);
        IList<SmallBunch> List();
        IList<SmallBunch> ListForUser();
        Bunch Add(Bunch bunch);
        Bunch Update(Bunch bunch);
        void Join(string id, string code);
    }
}