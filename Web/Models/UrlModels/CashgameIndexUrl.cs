﻿using Web.Routing;

namespace Web.Models.UrlModels
{
    public class CashgameIndexUrl : HomegameUrl
    {
        public CashgameIndexUrl(string slug)
            : base(RouteFormats.CashgameIndex, slug)
        {
        }
    }
}