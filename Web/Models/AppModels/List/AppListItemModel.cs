using Core.UseCases;
using Web.Common.Urls.SiteUrls;

namespace Web.Models.AppModels.List
{
    public class AppListItemModel
    {
        public string Name { get; private set; }
        public string Key { get; private set; }
        public string Url { get; private set; }

        public AppListItemModel(AppList.Item appListItem)
        {
            Name = appListItem.AppName;
            Key = appListItem.AppKey;
            Url = new AppDetailsUrl(appListItem.AppId).Relative;
        }
    }
}