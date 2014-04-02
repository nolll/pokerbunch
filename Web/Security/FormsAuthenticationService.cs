using System.Globalization;
using System.Web;
using System.Web.Security;
using Application.Services;
using Newtonsoft.Json;

namespace Web.Security
{
    public class FormsAuthenticationService : IAuthenticationService
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
    }
}