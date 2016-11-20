using System.Collections.Generic;
using Core.Entities;

namespace Core.Repositories
{
    public interface IAppRepository
    {
        App GetById(string id);
        IList<App> List();
        IList<App> ListByUser(string userId);
        string Add(App app);
        void Update(App app);
    }
}
