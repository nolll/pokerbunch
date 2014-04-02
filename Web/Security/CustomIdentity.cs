using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Web.Security;
using Core.Classes;
using Newtonsoft.Json;

namespace Web.Security
{
    [Serializable]
    public class CustomIdentity : IIdentity
    {
        private readonly bool _isAuthenticated;
        private readonly UserIdentity _user;

        public CustomIdentity(bool isAuthenticated, string userData)
        {
            _isAuthenticated = isAuthenticated;
            _user = JsonConvert.DeserializeObject<UserIdentity>(userData);
        }

        public CustomIdentity(FormsAuthenticationTicket ticket)
            : this(ticket != null, ticket.UserData)
        {
        }

        public string AuthenticationType
        {
            get { return "Custom"; }
        }

        public bool IsAuthenticated
        {
            get { return _isAuthenticated; }
        }

        public int UserId
        {
            get { return _user.UserId; }
        }

        public string Name
        {
            get { return _user.UserName; }
        }

        public string UserName
        {
            get { return _user.UserName; }
        }
        
        public string DisplayName
        {
            get { return _user.DisplayName; }
        }
        
        public bool IsAdmin
        {
            get { return _user.IsAdmin; }
        }

        public List<UserBunch> Bunches
        {
            get { return _user.Bunches; }
        }

        public Role GetRole(string slug)
        {
            foreach (var userBunch in _user.Bunches)
            {
                if (userBunch.Slug == slug)
                {
                    return userBunch.Role;
                }
            }
            return Role.None;
        }

        public bool IsInRole(string slug, Role roleToCheck)
        {
            if (IsAdmin)
            {
                return true;
            }
            var role = GetRole(slug);
            return (int)role >= (int)roleToCheck;
        }
    }
}