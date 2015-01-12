using System.Globalization;
using System.Web;
using System.Web.Security;
using Core;
using Core.Entities;
using Core.Services;
using Newtonsoft.Json;

namespace Infrastructure.Web
{
    public class Auth : IAuth
    {
        private readonly ITimeProvider _timeProvider;
        private const int Version = 1;

        public Auth(ITimeProvider timeProvider)
        {
            _timeProvider = timeProvider;
        }

        public void SignIn(UserIdentity user, bool createPersistentCookie)
        {
            var userData = JsonConvert.SerializeObject(user);

            var currentTime = _timeProvider.UtcNow;
            var expires = currentTime.AddYears(100);

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

        public CustomIdentity CurrentIdentity
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