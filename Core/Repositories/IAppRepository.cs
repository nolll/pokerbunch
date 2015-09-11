using System.Collections.Generic;
using Core.Entities;

namespace Core.Repositories
{
    public interface IAppRepository
    {
        IList<App> ListApps();
        IList<App> ListApps(int userId);
        App Get(int id);
        App Get(string appKey);
        IList<int> Find();
        IList<int> Find(int userId);
        IList<int> Find(string appKey);
        int Add(App app);
        void Update(App app);
    }
}
