using System.Collections.Generic;
using Core.Entities;

namespace Core.Repositories
{
    public interface IAppRepository
    {
        App Get(string id);
        IList<App> GetList(IList<string> ids);
        IList<string> Find();
        IList<string> FindByUser(string userId);
        IList<string> FindByAppKey(string appKey);
        string Add(App app);
        void Update(App app);
    }
}
