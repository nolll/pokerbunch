﻿namespace Web.Models.UrlModels
{
    public abstract class PlayerUrl : Url
    {
        protected PlayerUrl(string format, string slug, int playerId)
            : base(string.Format(format, slug, playerId))
        {
        }
    }
}