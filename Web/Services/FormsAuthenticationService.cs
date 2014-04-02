using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Application.Exceptions;
using Application.Services;
using Core.Classes;
using Newtonsoft.Json;

namespace Web.Services
{
    public interface IFormsAuthenticationService
    {
        void SignIn(UserIdentity user, bool createPersistentCookie);
        void SignOut();
    }

    public class FormsAuthenticationService : IFormsAuthenticationService
    {
        private readonly ITimeProvider _timeProvider;
        private const int Version = 1;

        public FormsAuthenticationService(
            ITimeProvider timeProvider)
        {
            _timeProvider = timeProvider;
        }

        public void SignIn(UserIdentity user, bool createPersistentCookie)
        {
            var userData = JsonConvert.SerializeObject(user);

            var currentTime = _timeProvider.GetTime();
            var expires = currentTime.AddMinutes(30);

            var authTicket = new FormsAuthenticationTicket(
                Version,
                user.UserId.ToString(CultureInfo.InvariantCulture),
                currentTime,
                expires,
                createPersistentCookie,
                userData);

            var encTicket = FormsAuthentication.Encrypt(authTicket);
            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket)
            {
                Expires = authTicket.Expiration,
                Path = FormsAuthentication.FormsCookiePath
            };

            if (HttpContext.Current != null)
            {
                HttpContext.Current.Response.Cookies.Add(cookie);
            }

        }

        public void SignOut()
        {
            FormsAuthentication.SignOut();
        }
    }

    public class UserIdentity
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string DisplayName { get; set; }
        public bool IsAdmin { get; set; }
        public List<UserBunch> Bunches { get; set; }
    }

    public class UserBunch
    {
        public string Slug { get; set; }
        public Role Role { get; set; }
    }

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

    public class CustomPrincipal : IPrincipal
    {
        public IIdentity Identity { get; private set; }

        public CustomPrincipal(IIdentity identity)
        {
            Identity = identity;
        }

        public bool IsInRole(string role)
        {
            return false;
        }
    }

    public class AuthorizeRoleAttribute : AuthorizeAttribute
    {
        public Role Role { get; set; }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var authorized = base.AuthorizeCore(httpContext);
            if (!authorized)
            {
                return false;
            }

            var identity = httpContext.User.Identity as CustomIdentity;
            if (identity == null)
            {
                return false;
            }

            if (Role == Role.Admin && identity.IsAdmin)
            {
                return true;
            }

            var slug = httpContext.Request.RequestContext.RouteData.Values["slug"] as string;
            return identity.Bunches.Any(userBunch => userBunch.Slug == slug && userBunch.Role >= Role);
        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);
            if (filterContext.HttpContext.User.Identity.IsAuthenticated && filterContext.Result is HttpUnauthorizedResult)
            {
                throw new AccessDeniedException();
            }
        }
    }
}