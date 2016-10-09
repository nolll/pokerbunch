using System.Collections.Generic;
using Core.Entities;

namespace Core.Repositories
{
    public interface IBunchRepository
    {
        Bunch Get(string slug);
        IList<Bunch> Get(IList<int> ids);
        IList<int> Search();
        IList<int> Search(string slug);
        IList<int> Search(int userId);
        int Add(Bunch bunch);
        void Update(Bunch bunch);
    }
}