﻿namespace Core.Urls
{
    public class AddCashgameUrl : BunchUrl
    {
        public AddCashgameUrl(string slug)
            : base(RouteFormats.CashgameAdd, slug)
        {
        }
    }
}