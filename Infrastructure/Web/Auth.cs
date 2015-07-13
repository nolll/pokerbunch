using System;
using System.Web;
using System.Web.Security;
using Core;
using Core.Entities;
using Core.Services;

namespace Infrastructure.Web
{
    public class Auth : IAuth
    {
        private const int Version = 2;

        public void SignIn(UserIdentity user, bool createPersistentCookie)
        {
            var currentTime = DateTime.UtcNow;
            var expires = currentTime.AddYears(100);

            var authTicket = new FormsAuthenticationTicket(
                Version,
                user.UserName,
                currentTime,
                expires,
                createPersistentCookie,
                "");

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

        private CustomIdentity CurrentIdentity
        {
            get
            {
                var identity = HttpContext.Current.User.Identity as CustomIdentity;
                return identity ?? new CustomIdentity();
            }
        }

        public bool IsInRole(string slug, Role role)
        {
            return CurrentIdentity.IsInRole(slug, role);
        }
    }
}