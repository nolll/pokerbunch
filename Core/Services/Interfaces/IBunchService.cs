using System.Collections.Generic;
using Core.Entities;

namespace Core.Services
{
    public interface IBunchService
    {
        Bunch Get(string id);
        IList<SmallBunch> List();
        IList<SmallBunch> ListForUser();
        Bunch Add(Bunch bunch);
        Bunch Update(Bunch bunch);
        void Join(string bunchId, string code);
    }
}