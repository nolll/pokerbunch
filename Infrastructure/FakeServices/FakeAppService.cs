using System.Collections.Generic;
using System.Linq;
using Core.Entities;
using Core.Services;

namespace Infrastructure.Api.FakeServices
{
    public class FakeAppService : IAppService
    {
        public App GetById(string id)
        {
            return FakeData.Apps.FirstOrDefault(o => o.Id == id);
        }

        public IList<App> ListAll()
        {
            return FakeData.Apps;
        }

        public IList<App> List()
        {
            return FakeData.Apps.Where(o => o.UserId == FakeData.CurrentUserId).ToList();
        }

        public string Add(string appName)
        {
            var id = appName.ToLower();
            var appKey = $"appkey_{id}";
            FakeData.Apps.Add(new App(id, appKey, appName, FakeData.CurrentUserId));
            return id;
        }

        public void Update(App app)
        {
            var index = FakeData.Apps.FindIndex(o => o.Id == app.Id);
            FakeData.Apps[index] = app;
        }

        public void Delete(string appId)
        {
            var index = FakeData.Apps.FindIndex(o => o.Id == appId);
            FakeData.Apps.RemoveAt(index);
        }
    }
}