﻿namespace Core.Urls
{
    public class EndCashgameUrl : BunchUrl
    {
        public EndCashgameUrl(string slug)
            : base(RouteFormats.CashgameEnd, slug)
        {
        }
    }
}