using Core.UseCases;
using Web.Urls.SiteUrls;

namespace Web.Models.AppModels.List
{
    public class AppListItemModel
    {
        public string Name { get; private set; }
        public string Key { get; private set; }
        public string Url { get; private set; }

        public AppListItemModel(AppListAll.Item i)
            : this(i.AppId, i.AppName, i.AppKey)
        {
        }

        public AppListItemModel(AppListUser.Item i)
            : this(i.AppId, i.AppName, i.AppKey)
        {

        }

        private AppListItemModel(string id, string name, string key)
        {
            Name = name;
            Key = key;
            Url = new AppDetailsUrl(id).Relative;
        }
    }
}