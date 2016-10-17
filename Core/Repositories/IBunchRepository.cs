using System.Collections.Generic;
using Core.Entities;

namespace Core.Repositories
{
    public interface IBunchRepository
    {
        Bunch Get(string slug);
        IList<Bunch> Get(IList<string> ids);
        IList<string> Search();
        IList<string> SearchBySlug(string slug);
        IList<string> SearchByUser(string userId);
        string Add(Bunch bunch);
        void Update(Bunch bunch);
    }
}