using System.Collections.Generic;
using Core.Entities;

namespace Core.Repositories
{
    public interface IBunchRepository
    {
        Bunch Get(string slug);
        IList<SmallBunch> Search();
        IList<SmallBunch> SearchByUser(string userName);
        Bunch Add(Bunch bunch);
        Bunch Update(Bunch bunch);
    }
}