using Core.Classes;
using Core.Services;
using Web.Models.UrlModels;

namespace Web.Services
{
    public class UrlProvider : IUrlProvider
    {
        public string GetJoinHomegameUrl(Homegame homegame)
        {
            var urlModel = new HomegameJoinUrlModel(homegame);
            return urlModel.Url;
        }

        public string GetAddUserUrl()
        {
            var urlModel = new UserAddUrlModel();
            return urlModel.Url;
        }
    }
}