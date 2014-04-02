using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Web.Security;
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
    }
}