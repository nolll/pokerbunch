﻿using Web.Routes;

namespace Web.Urls.SiteUrls
{
    public class TopListUrl : BunchWithOptionalYearUrl
    {
        public TopListUrl(string slug, int? year)
            : base(WebRoutes.Cashgame.Toplist, WebRoutes.Cashgame.ToplistWithYear, slug, year)
        {
        }
    }
}