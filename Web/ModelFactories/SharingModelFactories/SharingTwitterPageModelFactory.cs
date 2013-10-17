﻿using Core.Classes;
using Web.ModelFactories.PageBaseModelFactories;
using Web.Models.SharingModels;
using Web.Models.UrlModels;

namespace Web.ModelFactories.SharingModelFactories
{
    public class SharingTwitterPageModelFactory : ISharingTwitterPageModelFactory
    {
        private readonly IPagePropertiesFactory _pagePropertiesFactory;

        public SharingTwitterPageModelFactory(IPagePropertiesFactory pagePropertiesFactory)
        {
            _pagePropertiesFactory = pagePropertiesFactory;
        }

        public SharingTwitterPageModel Create(User user, bool isSharing, TwitterCredentials credentials)
        {
            var twitterName = isSharing && credentials != null ? credentials.TwitterName : null;

            return new SharingTwitterPageModel
                {
                    BrowserTitle = "Twitter Sharing",
                    PageProperties = _pagePropertiesFactory.Create(user),
                    IsSharing = isSharing,
                    TwitterName = twitterName,
                    PostUrl = GetPostUrlModel(isSharing)
                };
        }

        private UrlModel GetPostUrlModel(bool isSharing)
        {
            if (isSharing)
            {
                return new TwitterStopShareUrlModel();
            }
            return new TwitterStartShareUrlModel();
        }
    }
}