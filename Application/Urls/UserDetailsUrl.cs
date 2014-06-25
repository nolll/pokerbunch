﻿namespace Application.Urls
{
    public class UserDetailsUrl : UserUrl
    {
        public UserDetailsUrl(string userName)
            : base(RouteFormats.UserDetails, userName)
        {
        }
    }
}