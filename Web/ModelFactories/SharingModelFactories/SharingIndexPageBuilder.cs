using Application.Services;
using Application.Urls;
using Core.Entities;
using Core.Repositories;
using Web.ModelFactories.PageBaseModelFactories;
using Web.Models.SharingModels;

namespace Web.ModelFactories.SharingModelFactories
{
    public class SharingIndexPageBuilder : ISharingIndexPageBuilder
    {
        private readonly IPagePropertiesFactory _pagePropertiesFactory;
        private readonly IAuth _auth;
        private readonly ISharingRepository _sharingRepository;

        public SharingIndexPageBuilder(
            IPagePropertiesFactory pagePropertiesFactory,
            IAuth auth,
            ISharingRepository sharingRepository)
        {
            _pagePropertiesFactory = pagePropertiesFactory;
            _auth = auth;
            _sharingRepository = sharingRepository;
        }

        public SharingIndexPageModel Build()
        {
            var user = _auth.CurrentUser;
            var isSharing = _sharingRepository.IsSharing(user, SocialServiceIdentifier.Twitter);
            
            return new SharingIndexPageModel
                {
                    BrowserTitle = "Sharing",
                    PageProperties = _pagePropertiesFactory.Create(),
                    IsSharingToTwitter = isSharing,
			        ShareToTwitterSettingsUrl = new TwitterSettingsUrl()
                };
        }
    }
}