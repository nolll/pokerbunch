using Core.UseCases;
using PokerBunch.Common.Urls.SiteUrls;
using Web.Extensions;

namespace Web.Models.AppModels.List
{
    public class AppListItemModel : IViewModel
    {
        public string Name { get; }
        public string Key { get; }
        public string Url { get; }

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

        public View GetView()
        {
            return new View("~/Views/Pages/AppList/Item.cshtml");
        }
    }
}